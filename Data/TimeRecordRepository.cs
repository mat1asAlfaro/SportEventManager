using Microsoft.EntityFrameworkCore;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;

namespace SportEventManager.Data
{
    public class TimeRecordRepository : ITimeRecordRepository
    {
        private readonly SportEventDbContext _context;

        public TimeRecordRepository(SportEventDbContext context)
        {
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
    }
}