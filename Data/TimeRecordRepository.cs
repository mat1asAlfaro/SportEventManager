using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Hubs;
using SportEventManager.Models;
using SportEventManager.Core.Services;
using SportEventManager.DTOs;

namespace SportEventManager.Data
{
    public class TimeRecordRepository : ITimeRecordRepository
    {
        private readonly SportEventDbContext _context;
        private readonly IRegistrationRepository _registrationRepo;
        private readonly ISplitRepository _splitRepo;
        private readonly IRaceRepository _raceRepo;
        private readonly ITimingCalculationsService _calculationsService;
        private readonly IHubContext<TimingHub> _hubContext;
        private readonly ILogger<TimeRecordRepository> _logger;

        public TimeRecordRepository(
            IRegistrationRepository registrationRepo,
            ISplitRepository splitRepo,
            IRaceRepository raceRepo,
            ITimingCalculationsService calculationsService,
            IHubContext<TimingHub> hubContext,
            ILogger<TimeRecordRepository> logger,
            SportEventDbContext context)
        {
            _registrationRepo = registrationRepo;
            _splitRepo = splitRepo;
            _raceRepo = raceRepo;
            _calculationsService = calculationsService;
            _hubContext = hubContext;
            _logger = logger;
            _context = context;
        }

        public async Task<TimeRecord?> GetByIdAsync(int id)
        {
            return await _context.TimeRecords
                .Include(tr => tr.Chip)
                .Include(tr => tr.Race)
                .Include(tr => tr.Split)
                .FirstOrDefaultAsync(tr => tr.TimeRecordId == id);
        }

        public async Task<IEnumerable<TimeRecord>> GetByRaceIdAsync(int raceId)
        {
            return await _context.TimeRecords
                .Include(tr => tr.Chip)
                .Include(tr => tr.Split)
                .Where(tr => tr.RaceId == raceId)
                .OrderBy(tr => tr.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeRecord>> GetByChipIdAsync(int chipId)
        {
            return await _context.TimeRecords
                .Include(tr => tr.Race)
                .Include(tr => tr.Split)
                .Where(tr => tr.ChipId == chipId)
                .OrderBy(tr => tr.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeRecord>> GetBySplitIdAsync(int splitId)
        {
            return await _context.TimeRecords
                .Include(tr => tr.Chip)
                .Include(tr => tr.Race)
                .Where(tr => tr.SplitId == splitId)
                .OrderBy(tr => tr.Timestamp)
                .ToListAsync();
        }

        public async Task<TimeRecord?> GetByChipAndSplitAsync(int chipId, int splitId)
        {
            return await _context.TimeRecords
                .Include(tr => tr.Chip)
                .Include(tr => tr.Race)
                .Include(tr => tr.Split)
                .FirstOrDefaultAsync(tr => tr.ChipId == chipId && tr.SplitId == splitId);
        }

        public async Task<IEnumerable<TimeRecord>> GetAllAsync()
        {
            return await _context.TimeRecords
                .Include(tr => tr.Chip)
                .Include(tr => tr.Race)
                .Include(tr => tr.Split)
                .OrderBy(tr => tr.Timestamp)
                .ToListAsync();
        }

        public async Task<TimeRecord> AddAsync(TimeRecord timeRecord)
        {
            _context.TimeRecords.Add(timeRecord);
            await _context.SaveChangesAsync();
            return timeRecord;
        }

        public async Task<TimeRecord> UpdateAsync(TimeRecord timeRecord)
        {
            _context.Entry(timeRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return timeRecord;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timeRecord = await _context.TimeRecords.FindAsync(id);
            if (timeRecord == null)
                return false;

            _context.TimeRecords.Remove(timeRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int chipId, int splitId)
        {
            return await _context.TimeRecords
                .AnyAsync(tr => tr.ChipId == chipId && tr.SplitId == splitId);
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
                var existingRecord = await GetByChipAndSplitAsync(reading.ChipId, reading.SplitId);
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

                var savedRecord = await AddAsync(timeRecord);
                var recordWithDetails = await GetByIdAsync(savedRecord.TimeRecordId);

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
            var records = await GetByRaceIdAsync(raceId);
            return records.Select(MapToResponseDTO).ToList();
        }

        public async Task<RaceStatsDTO> GetRaceStatsAsync(int raceId)
        {
            var race = await _raceRepo.GetRaceByIdAsync(raceId);
            if (race == null)
                return new RaceStatsDTO { RaceId = raceId };

            var registrations = await _registrationRepo.GetByRaceIdAsync(raceId);
            var timeRecords = await GetByRaceIdAsync(raceId);
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

        private async Task<int> CalculateCurrentPosition(int raceId, double distanceCompleted, TimeSpan? elapsedTime)
        {
            // Obtener data de todos los corredores de la carrera
            var allRunners = await _context.Registrations
                .Where(r => r.RaceId == raceId)
                .Include(r => r.RegistrationChips)
                .Select(r => new
                {
                    r.BibNumber,
                    ChipIds = r.RegistrationChips!.Select(c => c.ChipId).ToList()
                })
                .ToListAsync();

            var runnerStats = new List<(int Bib, double Distance, TimeSpan Time)>();

            foreach (var runner in allRunners)
            {
                if (!runner.ChipIds.Any()) continue;

                var records = await _context.TimeRecords
                    .Include(tr => tr.Split)
                    .Where(tr => runner.ChipIds.Contains(tr.ChipId) &&
                                 tr.Split != null &&
                                 tr.Split.RaceId == raceId)
                    .OrderBy(tr => tr.Timestamp)
                    .ToListAsync();

                if (!records.Any()) continue;

                double km = records.Last().Split?.KmMark ?? 0;
                TimeSpan time = records.Last().Timestamp - records.First().Timestamp;

                runnerStats.Add((runner.BibNumber ?? 0, km, time));
            }

            // Ordenar por: mayor distancia → menor tiempo
            var ordered = runnerStats
                .OrderByDescending(r => r.Distance)
                .ThenBy(r => r.Time)
                .ToList();

            // Buscar posición del corredor solicitado
            var pos = ordered.FindIndex(r =>
                Math.Abs(r.Distance - distanceCompleted) < 0.001 &&
                r.Time == elapsedTime) + 1;

            return pos > 0 ? pos : ordered.Count;
        }

        public async Task<LiveParticipantDataDTO?> GetLiveParticipantDataByBibAsync(int raceId, int bibNumber)
        {
            // ============
            // 1. Obtener inscripción
            // ============
            var registration = await _context.Registrations
                .Include(r => r.Participant)
                .Include(r => r.Category)
                .Include(r => r.Race).ThenInclude(r => r!.Event)
                .Include(r => r.RegistrationChips)
                .FirstOrDefaultAsync(r =>
                    r.RaceId == raceId &&
                    r.BibNumber == bibNumber);

            if (registration == null)
                return null;

            var dto = new LiveParticipantDataDTO
            {
                ParticipantName = $"{registration.Participant?.FirstName} {registration.Participant?.LastName}".Trim(),
                EventName = registration.Race?.Event?.Name ?? "N/A",
                BibNumber = bibNumber,
                RaceName = registration.Race?.Name ?? "N/A",
                CategoryName = registration.Category?.ExternalName ??
                               registration.Category?.InternalName ?? "N/A",
                Status = "No iniciado"
            };

            // Distancia oficial → OPCIÓN A (tu elección)
            double totalRaceKm = registration.Race?.DistanceKm ?? 0;

            // ============
            // 2. Chips
            // ============
            var chipIds = registration.RegistrationChips?.Select(c => c.ChipId).ToList() ?? new();

            if (!chipIds.Any())
                return dto;

            // ============
            // 3. TimeRecords + Splits
            // ============
            var timeRecords = await _context.TimeRecords
                .Where(tr => chipIds.Contains(tr.ChipId) &&
                             tr.Split != null &&
                             tr.Split.RaceId == raceId)
                .Include(tr => tr.Split)
                .OrderBy(tr => tr.Timestamp)
                .ToListAsync();

            if (!timeRecords.Any())
                return dto;

            var first = timeRecords.First();
            var last = timeRecords.Last();

            dto.DistanceCompleted = last.Split?.KmMark ?? 0;
            dto.ElapsedTime = last.Timestamp - first.Timestamp;

            // ============
            // Ritmo promedio
            // ============
            if (dto.DistanceCompleted > 0 && dto.ElapsedTime.Value.TotalSeconds > 0)
            {
                var avgMin = dto.ElapsedTime.Value.TotalMinutes / dto.DistanceCompleted;
                dto.AveragePace = TimeSpan.FromMinutes(avgMin);
            }

            // ============
            // Ritmo actual
            // ============
            if (timeRecords.Count >= 2)
            {
                var prev = timeRecords[^2];
                var curr = timeRecords[^1];

                double kmDiff = (curr.Split?.KmMark ?? 0) - (prev.Split?.KmMark ?? 0);
                double minDiff = (curr.Timestamp - prev.Timestamp).TotalMinutes;

                if (kmDiff > 0 && minDiff > 0)
                    dto.CurrentPace = TimeSpan.FromMinutes(minDiff / kmDiff);
            }

            // ============
            // 4. Splits
            // ============
            var splitList = await _context.Splits
                .Where(s => s.RaceId == raceId)
                .OrderBy(s => s.KmMark)
                .ToListAsync();

            int completedSplits = timeRecords.Select(tr => tr.SplitId).Distinct().Count();
            int totalSplits = splitList.Count;

            // ============
            // Estado
            // ============
            dto.Status = completedSplits switch
            {
                0 => "No iniciado",
                _ when dto.DistanceCompleted >= totalRaceKm => "Finalizado",
                _ => "En Carrera"
            };

            // ============
            // Posición actual
            // ============
            dto.CurrentPosition = await CalculateCurrentPosition(
                raceId,
                dto.DistanceCompleted,
                dto.ElapsedTime
            );

            // ============
            // Porcentajes
            // ============
            dto.ProgressPercentage =
                totalRaceKm > 0 ? Math.Round(dto.InterpolatedDistance / totalRaceKm * 100, 2) : 0;

            dto.SplitsPercentage =
                totalSplits > 0 ? Math.Round((double)completedSplits / totalSplits * 100, 2) : 0;

            // ============
            // Progreso por split
            // ============
            dto.SplitsProgress = splitList.Select(sp =>
            {
                var tr = timeRecords.FirstOrDefault(t => t.SplitId == sp.SplitId);

                return new SplitProgressDTO
                {
                    SplitName = sp.SplitName,
                    KmMark = sp.KmMark,
                    Passed = tr != null,
                    Timestamp = tr?.Timestamp
                };
            }).ToList();

            // ============
            // 5. ETA
            // ============
            if (dto.DistanceCompleted > 0 && dto.DistanceCompleted < totalRaceKm)
            {
                double kmLeft = totalRaceKm - dto.DistanceCompleted;
                var pace = dto.CurrentPace ?? dto.AveragePace;

                if (pace is not null && pace.Value.TotalMinutes > 0)
                {
                    dto.EstimatedTimeToFinish = TimeSpan.FromMinutes(pace.Value.TotalMinutes * kmLeft);
                    dto.EstimatedFinishDateTime = DateTime.UtcNow + dto.EstimatedTimeToFinish;
                }

                dto.DistanceLeft = kmLeft;
            }

            // ============
            // 6. Interpolación
            // ============
            var lastRecord = last;
            double lastKm = lastRecord.Split?.KmMark ?? 0;
            var interpolationPace = dto.CurrentPace ?? dto.AveragePace;

            if (interpolationPace is null || interpolationPace.Value.TotalMinutes <= 0)
            {
                dto.InterpolatedDistance = lastKm;
            }
            else
            {
                double minutesSince = (DateTime.UtcNow - lastRecord.Timestamp).TotalMinutes;
                double kmExtra = minutesSince / interpolationPace.Value.TotalMinutes;

                var nextSplit = splitList.FirstOrDefault(s => s.KmMark > lastKm);
                double nextKm = nextSplit?.KmMark ?? totalRaceKm;

                double interp = Math.Min(lastKm + kmExtra, nextKm);
                dto.InterpolatedDistance = Math.Min(interp, totalRaceKm);
            }

            dto.ProgressPercentageInterpolated =
                totalRaceKm > 0 ? Math.Round(dto.InterpolatedDistance / totalRaceKm * 100, 2) : 0;

            return dto;
        }

        private async Task BroadcastTimeUpdate(TimeRecord record)
        {
            try
            {
                var registration = await GetRegistrationByChipId(record.ChipId, record.RaceId);
                var splitRecords = await GetBySplitIdAsync(record.SplitId);
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
            var allRecords = await GetByChipIdAsync(chipId);
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