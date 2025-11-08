using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.DTOs;
using SportEventManager.Models;
using Microsoft.EntityFrameworkCore;

namespace SportEventManager.Data
{
    public class SplitRepository : ISplitRepository
    {
        private readonly SportEventDbContext _context;

        public SplitRepository(SportEventDbContext context)
        {
            _context = context;
        }

        public async Task<List<Split>> GetAllSplitsAsync()
        {
            return await Task.FromResult(_context.Splits.ToList());
        }

        public async Task<Split?> GetSplitByIdAsync(int splitId)
        {
            var split = _context.Splits.FirstOrDefault(s => s.SplitId == splitId);
            return await Task.FromResult(split);
        }

        public async Task AddSplitAsync(Split split)
        {
            _context.Splits.Add(split);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSplitAsync(Split split)
        {
            var existingSplit = _context.Splits.FirstOrDefault(s => s.SplitId == split.SplitId);
            if (existingSplit != null)
            {
                existingSplit.SplitName = split.SplitName;
                existingSplit.KmMark = split.KmMark;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSplitAsync(int splitId)
        {
            var split = _context.Splits.FirstOrDefault(s => s.SplitId == splitId);
            if (split != null)
            {
                _context.Splits.Remove(split);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Split?> GetByIdAsync(int splitId)
        {
            return await GetSplitByIdAsync(splitId);
        }
        public async Task<IEnumerable<Split>> GetByRaceIdAsync(int raceId)
        {
            return await _context.Splits
                .Where(s => s.RaceId == raceId)
                .OrderBy(s => s.KmMark)
                .ToListAsync();
        }
    }
}