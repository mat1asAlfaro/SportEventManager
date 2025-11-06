using Microsoft.AspNetCore.SignalR;
using SportEventManager.Core;
using SportEventManager.DTOs;
using SportEventManager.Hubs;
using SportEventManager.Models;

namespace SportEventManager.Services
{
    /// Interfaz del servicio principal de timing
    public interface ITimingService
    {
        Task<TimeRecordResponseDTO?> RegisterChipReadingAsync(ChipReadingDTO reading);
        Task<IEnumerable<TimeRecordResponseDTO>> GetTimeRecordsByRaceAsync(int raceId);
        Task<RaceStatsDTO> GetRaceStatsAsync(int raceId);
    }

    /// Servicio principal para gestión del sistema de timing RFID
    public class TimingService : ITimingService
    {
        private readonly ITimeRecordRepository _timeRecordRepo;
        private readonly IRegistrationRepository _registrationRepo;
        private readonly ISplitRepository _splitRepo;
        private readonly IRaceRepository _raceRepo;
        private readonly ITimingCalculationsService _calculationsService;
        private readonly IHubContext<TimingHub> _hubContext;
        private readonly ILogger<TimingService> _logger;

        public TimingService(
            ITimeRecordRepository timeRecordRepo,
            IRegistrationRepository registrationRepo,
            ISplitRepository splitRepo,
            IRaceRepository raceRepo,
            ITimingCalculationsService calculationsService,
            IHubContext<TimingHub> hubContext,
            ILogger<TimingService> logger)
        {
            _timeRecordRepo = timeRecordRepo;
            _registrationRepo = registrationRepo;
            _splitRepo = splitRepo;
            _raceRepo = raceRepo;
            _calculationsService = calculationsService;
            _hubContext = hubContext;
            _logger = logger;
        }

        /// Registra una lectura de chip RFID y transmite en tiempo real
        public async Task<TimeRecordResponseDTO?> RegisterChipReadingAsync(ChipReadingDTO reading)
        {
            try
            {
                // Validar que el split existe
                var split = await _splitRepo.GetByIdAsync(reading.SplitId);
                if (split == null)
                {
                    _logger.LogWarning($"Split {reading.SplitId} not found");
                    return null;
                }

                var raceId = split.RaceId;

                // Verificar si ya existe un registro para este chip en este split (evitar duplicados)
                var existingRecord = await _timeRecordRepo.GetByChipAndSplitAsync(reading.ChipId, reading.SplitId);
                if (existingRecord != null)
                {
                    _logger.LogWarning($"Duplicate reading: ChipId {reading.ChipId} at SplitId {reading.SplitId}");
                    return MapToResponseDTO(existingRecord);
                }

                // Crear el nuevo registro de tiempo
                var timeRecord = new TimeRecord
                {
                    ChipId = reading.ChipId,
                    RaceId = raceId,
                    SplitId = reading.SplitId,
                    Timestamp = reading.Timestamp ?? DateTime.UtcNow
                };

                // Guardar en la base de datos
                var savedRecord = await _timeRecordRepo.AddAsync(timeRecord);

                // Cargar las relaciones para el DTO
                var recordWithDetails = await _timeRecordRepo.GetByIdAsync(savedRecord.TimeRecordId);
                
                if (recordWithDetails == null)
                    return null;

                var responseDTO = MapToResponseDTO(recordWithDetails);

                // Transmitir actualización en tiempo real via SignalR
                await BroadcastTimeUpdate(recordWithDetails);

                _logger.LogInformation($"Time recorded: ChipId {reading.ChipId}, SplitId {reading.SplitId}, RaceId {raceId}");

                return responseDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error registering chip reading");
                throw;
            }
        }

        /// Obtiene todos los registros de tiempo de una carrera
        public async Task<IEnumerable<TimeRecordResponseDTO>> GetTimeRecordsByRaceAsync(int raceId)
        {
            var records = await _timeRecordRepo.GetByRaceIdAsync(raceId);
            return records.Select(MapToResponseDTO);
        }

        /// Obtiene estadísticas en tiempo real de una carrera
        public async Task<RaceStatsDTO> GetRaceStatsAsync(int raceId)
        {
            var race = await _raceRepo.GetByIdAsync(raceId);
            if (race == null)
                return new RaceStatsDTO { RaceId = raceId };

            var registrations = await _registrationRepo.GetByRaceIdAsync(raceId);
            var timeRecords = await _timeRecordRepo.GetByRaceIdAsync(raceId);
            var splits = await _splitRepo.GetByRaceIdAsync(raceId);

            var stats = new RaceStatsDTO
            {
                RaceId = raceId,
                RaceName = race.Name,
                TotalParticipants = registrations.Count(),
                ParticipantsStarted = timeRecords.Select(tr => tr.ChipId).Distinct().Count(),
                ParticipantsBySplit = new Dictionary<int, int>()
            };

            // Contar participantes por cada split
            foreach (var split in splits)
            {
                var count = timeRecords.Count(tr => tr.SplitId == split.SplitId);
                stats.ParticipantsBySplit[split.SplitId] = count;
            }

            // Identificar el último split como meta y contar finalizados
            var finalSplit = splits.OrderByDescending(s => s.KmMark).FirstOrDefault();
            if (finalSplit != null)
            {
                stats.ParticipantsFinished = timeRecords.Count(tr => tr.SplitId == finalSplit.SplitId);
            }

            return stats;
        }

        /// Transmite una actualización en tiempo real via SignalR
        private async Task BroadcastTimeUpdate(TimeRecord record)
        {
            try
            {
                // Obtener información del participante
                var registration = await GetRegistrationByChipId(record.ChipId, record.RaceId);
                
                // Calcular posición en el split
                var splitRecords = await _timeRecordRepo.GetBySplitIdAsync(record.SplitId);
                var position = _calculationsService.CalculatePosition(splitRecords.ToList(), record.TimeRecordId);

                // Calcular tiempo desde el inicio (si existe registro en el primer split)
                TimeSpan? timeFromStart = await CalculateTimeFromStart(record.ChipId, record.RaceId, record.Timestamp);

                var liveUpdate = new LiveTimeUpdateDTO
                {
                    RaceId = record.RaceId,
                    SplitId = record.SplitId,
                    SplitName = record.Split?.SplitName,
                    KmMark = record.Split?.KmMark,
                    ParticipantName = registration != null 
                        ? $"{registration.Participant?.FirstName} {registration.Participant?.LastName}" 
                        : "Unknown",
                    ChipSerialNumber = record.Chip?.SerialNumber,
                    Timestamp = record.Timestamp,
                    Position = position,
                    TimeFromStart = timeFromStart
                };

                // Enviar a todos los clientes suscritos a esta carrera
                await _hubContext.Clients.Group($"race_{record.RaceId}")
                    .SendAsync("ReceiveTimeUpdate", liveUpdate);
                
                // Enviar a todos los clientes suscritos a este split específico
                await _hubContext.Clients.Group($"split_{record.SplitId}")
                    .SendAsync("ReceiveSplitUpdate", liveUpdate);

                // Enviar estadísticas actualizadas
                var stats = await GetRaceStatsAsync(record.RaceId);
                await _hubContext.Clients.Group($"race_{record.RaceId}")
                    .SendAsync("ReceiveRaceStats", stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error broadcasting time update");
            }
        }

        /// Obtiene la inscripción asociada a un chip
        private async Task<Registration?> GetRegistrationByChipId(int chipId, int raceId)
        {
            var registrations = await _registrationRepo.GetByRaceIdAsync(raceId);
            return registrations.FirstOrDefault(r => 
                r.RegistrationChips?.Any(rc => rc.ChipId == chipId) ?? false);
        }

        /// Calcula el tiempo transcurrido desde el inicio de la carrera
        
        private async Task<TimeSpan?> CalculateTimeFromStart(int chipId, int raceId, DateTime currentTime)
        {
            // Obtener todos los registros de este chip en esta carrera
            var allRecords = await _timeRecordRepo.GetByChipIdAsync(chipId);
            var raceRecords = allRecords.Where(tr => tr.RaceId == raceId).OrderBy(tr => tr.Timestamp);

            var firstRecord = raceRecords.FirstOrDefault();
            if (firstRecord != null)
            {
                return currentTime - firstRecord.Timestamp;
            }

            return null;
        }

        /// Mapea un TimeRecord a su DTO de respuesta con información completa        
        private TimeRecordResponseDTO MapToResponseDTO(TimeRecord record)
        {
            var registration = GetRegistrationByChipId(record.ChipId, record.RaceId).Result;

            return new TimeRecordResponseDTO
            {
                TimeRecordId = record.TimeRecordId,
                ChipId = record.ChipId,
                ChipSerialNumber = record.Chip?.SerialNumber,
                RaceId = record.RaceId,
                RaceName = record.Race?.Name,
                SplitId = record.SplitId,
                SplitName = record.Split?.SplitName,
                KmMark = record.Split?.KmMark,
                Timestamp = record.Timestamp,
                ParticipantName = registration != null 
                    ? $"{registration.Participant?.FirstName} {registration.Participant?.LastName}" 
                    : null,
                RegistrationId = registration?.RegistrationId
            };
        }
    }
}