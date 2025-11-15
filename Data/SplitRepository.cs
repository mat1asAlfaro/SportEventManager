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

        public async Task<SplitDTO?> GetByIdAsync(int splitId)
        {
            var split = await _context.Splits
                .Include(r => r.Race)
                .Where(s => s.SplitId == splitId)
                .FirstOrDefaultAsync();

            if (split == null)
            {
                return null;
            }

            var splitDTO = new SplitDTO
            {
                SplitId = split.SplitId,
                RaceId = split.RaceId,
                SplitName = split.SplitName,
                KmMark = split.KmMark,
                Race = split.Race is null ? null : new RaceDTO
                {
                    RaceId = split.Race.RaceId
                }
            };

            return splitDTO;
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

        public async Task<IEnumerable<Split>> GetByRaceIdAsync(int raceId)
        {
            return await _context.Splits
                .Where(s => s.RaceId == raceId)
                .OrderBy(s => s.KmMark)
                .ToListAsync();
        }
    }
}