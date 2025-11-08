using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core;
using SportEventManager.DTOs;
using SportEventManager.Hubs;
using SportEventManager.Models;
using SportEventManager.Data.Persistence;

namespace SportEventManager.Services
{
    public class TimingService : ITimingService
    {
        private readonly ITimeRecordRepository _timeRecordRepo;
        private readonly IRegistrationRepository _registrationRepo;
        private readonly ISplitRepository _splitRepo;
        private readonly IRaceRepository _raceRepo;
        private readonly ITimingCalculationsService _calculationsService;
        private readonly IHubContext<TimingHub> _hubContext;
        private readonly ILogger<TimingService> _logger;
        private readonly SportEventDbContext _context;

        public TimingService(
            ITimeRecordRepository timeRecordRepo,
            IRegistrationRepository registrationRepo,
            ISplitRepository splitRepo,
            IRaceRepository raceRepo,
            ITimingCalculationsService calculationsService,
            IHubContext<TimingHub> hubContext,
            ILogger<TimingService> logger,
            SportEventDbContext context)
        {
            _timeRecordRepo = timeRecordRepo;
            _registrationRepo = registrationRepo;
            _splitRepo = splitRepo;
            _raceRepo = raceRepo;
            _calculationsService = calculationsService;
            _hubContext = hubContext;
            _logger = logger;
            _context = context;
        }

        public async Task<TimeRecordResponseDTO?> RegisterChipReadingAsync(ChipReadingDTO reading)
        {
            try
            {
                var split = await _splitRepo.GetByIdAsync(reading.SplitId);
                if (split == null)
                {
                    _logger.LogWarning($"Split {reading.SplitId} not found");
                    return null;
                }

                var raceId = split.RaceId;

                // Evitar registros duplicados
                var existingRecord = await _timeRecordRepo.GetByChipAndSplitAsync(reading.ChipId, reading.SplitId);
                if (existingRecord != null)
                {
                    _logger.LogWarning($"Duplicate reading: ChipId {reading.ChipId} at SplitId {reading.SplitId}");
                    return MapToResponseDTO(existingRecord);
                }

                var timeRecord = new TimeRecord
                {
                    ChipId = reading.ChipId,
                    RaceId = raceId,
                    SplitId = reading.SplitId,
                    Timestamp = reading.Timestamp ?? DateTime.UtcNow
                };

                var savedRecord = await _timeRecordRepo.AddAsync(timeRecord);
                var recordWithDetails = await _timeRecordRepo.GetByIdAsync(savedRecord.TimeRecordId);

                if (recordWithDetails == null)
                    return null;

                var responseDTO = MapToResponseDTO(recordWithDetails);
                await BroadcastTimeUpdate(recordWithDetails);

                _logger.LogInformation($"Time recorded: ChipId {reading.ChipId}, SplitId {reading.SplitId}, RaceId {raceId}");

                return responseDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering chip reading");
                throw;
            }
        }

        public async Task<List<TimeRecordResponseDTO>> GetTimeRecordsByRaceAsync(int raceId)
        {
            var records = await _timeRecordRepo.GetByRaceIdAsync(raceId);
            return records.Select(MapToResponseDTO).ToList();
        }

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

            foreach (var split in splits)
            {
                var count = timeRecords.Count(tr => tr.SplitId == split.SplitId);
                stats.ParticipantsBySplit[split.SplitId] = count;
            }

            var finalSplit = splits.OrderByDescending(s => s.KmMark).FirstOrDefault();
            if (finalSplit != null)
            {
                stats.ParticipantsFinished = timeRecords.Count(tr => tr.SplitId == finalSplit.SplitId);
            }

            return stats;
        }

        public async Task<LiveParticipantDataDTO?> GetLiveParticipantDataByBibAsync(int raceId, int bibNumber)
        {
            var registration = await _context.Registrations
                .Include(r => r.Participant)
                .Include(r => r.Category)
                .Include(r => r.Race)
                .Include(r => r.RegistrationChips)
                .FirstOrDefaultAsync(r => r.RaceId == raceId && r.BibNumber.HasValue && r.BibNumber.Value == bibNumber);

            if (registration == null)
            {
                _logger.LogWarning($"No registration found for bib {bibNumber} in race {raceId}");
                return null;
            }

            var dto = new LiveParticipantDataDTO
            {
                ParticipantName = $"{registration.Participant?.FirstName} {registration.Participant?.LastName}".Trim(),
                BibNumber = bibNumber,
                RaceName = registration.Race?.Name ?? "N/A",
                CategoryName = registration.Category?.ExternalName ?? registration.Category?.InternalName ?? "N/A",
                Status = "No iniciado",
                DistanceCompleted = 0
            };

            var chipIds = registration.RegistrationChips?.Select(rc => rc.ChipId).ToList() ?? new List<int>();

            if (!chipIds.Any())
            {
                _logger.LogWarning($"No chips assigned to bib {bibNumber}");
                return dto;
            }

            var timeRecords = await _context.TimeRecords
                .Include(tr => tr.Split)
                .Where(tr => chipIds.Contains(tr.ChipId) && tr.Split != null && tr.Split.RaceId == raceId)
                .OrderBy(tr => tr.Timestamp)
                .ToListAsync();

            if (!timeRecords.Any())
                return dto;

            var firstRecord = timeRecords.First();
            var lastRecord = timeRecords.Last();

            dto.ElapsedTime = lastRecord.Timestamp - firstRecord.Timestamp;
            dto.DistanceCompleted = lastRecord.Split?.KmMark ?? 0;

            // Calcular ritmo promedio
            if (dto.DistanceCompleted > 0)
            {
                var totalMinutes = dto.ElapsedTime.Value.TotalMinutes;
                var avgPaceMinutes = totalMinutes / dto.DistanceCompleted;
                dto.AveragePace = TimeSpan.FromMinutes(avgPaceMinutes);
            }

            // Determinar estado
            var totalSplits = await _context.Splits.CountAsync(s => s.RaceId == raceId);
            var completedSplits = timeRecords.Count;

            if (completedSplits == totalSplits)
                dto.Status = "Finalizado";
            else if (completedSplits > 0)
                dto.Status = "En Carrera";

            return dto;
        }

        private async Task BroadcastTimeUpdate(TimeRecord record)
        {
            try
            {
                var registration = await GetRegistrationByChipId(record.ChipId, record.RaceId);
                var splitRecords = await _timeRecordRepo.GetBySplitIdAsync(record.SplitId);
                var position = _calculationsService.CalculatePosition(splitRecords.ToList(), record.TimeRecordId);
                var timeFromStart = await CalculateTimeFromStart(record.ChipId, record.RaceId, record.Timestamp);

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

                await _hubContext.Clients.Group($"race_{record.RaceId}")
                    .SendAsync("ReceiveTimeUpdate", liveUpdate);

                await _hubContext.Clients.Group($"split_{record.SplitId}")
                    .SendAsync("ReceiveSplitUpdate", liveUpdate);

                var stats = await GetRaceStatsAsync(record.RaceId);
                await _hubContext.Clients.Group($"race_{record.RaceId}")
                    .SendAsync("ReceiveRaceStats", stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error broadcasting time update");
            }
        }

        private async Task<Registration?> GetRegistrationByChipId(int chipId, int raceId)
        {
            var registrations = await _registrationRepo.GetByRaceIdAsync(raceId);
            return registrations.FirstOrDefault(r =>
                r.RegistrationChips?.Any(rc => rc.ChipId == chipId) ?? false);
        }

        private async Task<TimeSpan?> CalculateTimeFromStart(int chipId, int raceId, DateTime currentTime)
        {
            var allRecords = await _timeRecordRepo.GetByChipIdAsync(chipId);
            var raceRecords = allRecords.Where(tr => tr.RaceId == raceId).OrderBy(tr => tr.Timestamp);

            var firstRecord = raceRecords.FirstOrDefault();
            if (firstRecord != null)
            {
                return currentTime - firstRecord.Timestamp;
            }

            return null;
        }

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