using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;

namespace SportEventManager.Data
{
  public class RaceRepository : IRaceRepository
  {
    private readonly SportEventDbContext _context;

    public RaceRepository(SportEventDbContext context)
    {
      _context = context;
    }

    public async Task<List<Race>> GetAllRacesAsync()
    {
      return await Task.FromResult(_context.Races.ToList());
    }

    public async Task<Race?> GetRaceByIdAsync(int raceId)
    {
      var race = _context.Races.FirstOrDefault(r => r.RaceId == raceId);
      return await Task.FromResult(race);
    }

    public async Task AddRaceAsync(Race race)
    {
      _context.Races.Add(race);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateRaceAsync(Race race)
    {
      var existingRace = _context.Races.FirstOrDefault(r => r.RaceId == race.RaceId);
      if (existingRace != null)
      {
        existingRace.Name = race.Name;
        existingRace.DistanceKm = race.DistanceKm;
        existingRace.MaxParticipants = race.MaxParticipants;
        existingRace.StartTime = race.StartTime;

        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteRaceAsync(int raceId)
    {
      var race = _context.Races.FirstOrDefault(r => r.RaceId == raceId);
      if (race != null)
      {
        _context.Races.Remove(race);
        await _context.SaveChangesAsync();
      }
    }
  }
}