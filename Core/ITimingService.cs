using SportEventManager.DTOs;

namespace SportEventManager.Core
{
    public interface ITimingService
    {
        Task<TimeRecordResponseDTO?> RegisterChipReadingAsync(ChipReadingDTO reading);
        Task<List<TimeRecordResponseDTO>> GetTimeRecordsByRaceAsync(int raceId);
        Task<RaceStatsDTO> GetRaceStatsAsync(int raceId);
        Task<LiveParticipantDataDTO?> GetLiveParticipantDataByBibAsync(int raceId, int bibNumber);
    }
}