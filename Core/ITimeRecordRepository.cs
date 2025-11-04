using SportEventManager.Models;

namespace SportEventManager.Core
{
    public interface ITimeRecordRepository
    {
        Task<TimeRecord?> GetByIdAsync(int id);
        Task<IEnumerable<TimeRecord>> GetByRaceIdAsync(int raceId);
        Task<IEnumerable<TimeRecord>> GetByChipIdAsync(int chipId);
        Task<IEnumerable<TimeRecord>> GetBySplitIdAsync(int splitId);
        Task<TimeRecord?> GetByChipAndSplitAsync(int chipId, int splitId);
        Task<IEnumerable<TimeRecord>> GetAllAsync();
        Task<TimeRecord> AddAsync(TimeRecord timeRecord);
        Task<TimeRecord> UpdateAsync(TimeRecord timeRecord);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int chipId, int splitId);
    }
}