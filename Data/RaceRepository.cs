using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;

namespace SportEventManager.Data
{
  public class RaceRepository : IRaceRepository
  {
    private readonly IDbContextFactory<SportEventDbContext> _contextFactory;

    public RaceRepository(IDbContextFactory<SportEventDbContext> contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public async Task<List<Race>> GetAllRacesAsync()
    {
      await using var _context = _contextFactory.CreateDbContext();
      return await Task.FromResult(_context.Races.ToList());
    }

    public async Task<Race?> GetRaceByIdAsync(int raceId)
    {
      await using var _context = _contextFactory.CreateDbContext();
      return await _context.Races
        .Include("RaceCategories.Category")
        .FirstOrDefaultAsync(r => r.RaceId == raceId);
    }

    public async Task<Race?> GetRaceWithCategoriesByIdAsync(int raceId)
    {
      await using var _context = _contextFactory.CreateDbContext();
      return await _context.Races
        .Include(r => r.RaceCategories)
        .FirstOrDefaultAsync(r => r.RaceId == raceId);
    }

    public async Task AddRaceAsync(Race race)
    {
      await using var _context = _contextFactory.CreateDbContext();
      _context.Races.Add(race);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateRaceAsync(Race race)
    {
      await using var _context = _contextFactory.CreateDbContext();

      var existingRace = await _context.Races
          .Include(r => r.RaceCategories)
          .FirstOrDefaultAsync(r => r.RaceId == race.RaceId);

      if (existingRace == null) throw new Exception("Race not found");

      var newCategoryIds = race.RaceCategories!.Select(rc => rc.CategoryId).ToList();

      var toRemove = existingRace.RaceCategories!
          .Where(rc => !newCategoryIds.Contains(rc.CategoryId))
          .ToList();
      _context.RaceCategories.RemoveRange(toRemove);

      var existingIds = existingRace.RaceCategories!.Select(rc => rc.CategoryId).ToList();
      var toAdd = newCategoryIds
          .Where(id => !existingIds.Contains(id))
          .Select(id => new RaceCategory { RaceId = existingRace.RaceId, CategoryId = id })
          .ToList();
      _context.RaceCategories.AddRange(toAdd);

      await _context.SaveChangesAsync();
    }

    public async Task DeleteRaceAsync(int raceId)
    {
      await using var _context = _contextFactory.CreateDbContext();
      var race = _context.Races.FirstOrDefault(r => r.RaceId == raceId);
      if (race != null)
      {
        _context.Races.Remove(race);
        await _context.SaveChangesAsync();
      }
    }
    public async Task<Race?> GetByIdAsync(int raceId)
    {
      return await GetRaceByIdAsync(raceId);
    }
  }
}