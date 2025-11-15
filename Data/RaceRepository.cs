using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;
using SportEventManager.DTOs;

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

    public async Task<List<Race>> GetRacesByEventIdAsync(int eventId)
    {
      await using var _context = _contextFactory.CreateDbContext();
      return await _context.Races
        .Where(r => r.EventId == eventId)
        .ToListAsync();
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
      Console.WriteLine($"UPDATE RACE: {race.MaxParticipants}");
      await using var _context = _contextFactory.CreateDbContext();

      var existingRace = await _context.Races
          .Include(r => r.RaceCategories)
          .FirstOrDefaultAsync(r => r.RaceId == race.RaceId);

      if (existingRace == null) throw new Exception("Race not found");

      existingRace.Name = race.Name;
      existingRace.DistanceKm = race.DistanceKm;
      existingRace.MaxParticipants = race.MaxParticipants;
      existingRace.StartTime = race.StartTime;
      existingRace.EventId = race.EventId;

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
    public async Task<RaceDTO?> GetRaceDTOByIdAsync(int raceId)
{
  await using var _context = _contextFactory.CreateDbContext();

  var race = await _context.Races
    .Include(r => r.Splits!)
    .Include(r => r.TimeRecords!)
        .ThenInclude(tr => tr.Split)
    .FirstOrDefaultAsync(r => r.RaceId == raceId);

  if (race == null)
    return null;

  var eventName = await _context.Events
      .Where(e => e.EventId == race.EventId)
      .Select(e => e.Name)
      .FirstOrDefaultAsync();

  // Cargar registraciones con proyección incluyendo el nombre del participante
  var registrations = await _context.Registrations
      .Where(rg => rg.RaceId == raceId)
      .Select(rg => new RegistrationDTO
      {
        RegistrationId = rg.RegistrationId,
        ParticipantId = rg.ParticipantId,
        RaceId = rg.RaceId,
        CategoryId = rg.CategoryId,
        BibNumber = rg.BibNumber,
        Status = rg.Status,
        ParticipantName = rg.Participant != null 
            ? (rg.Participant.FirstName + " " + rg.Participant.LastName).Trim()
            : null, 
        RegistrationChips = rg.RegistrationChips!.Select(c => new RegistrationChipDTO
        {
          RegistrationChipId = c.RegistrationChipId,
          RegistrationId = c.RegistrationId,
          ChipId = c.ChipId,
        }).ToList()
      })
      .ToListAsync();

  return new RaceDTO
  {
    RaceId = race.RaceId,
    EventId = race.EventId,
    RaceName = race.Name,
    EventName = eventName,
    DistanceKm = race.DistanceKm,
    MaxParticipants = race.MaxParticipants,
    StartTime = race.StartTime,
    TotalParticipantRegistration = registrations.Count,

    Registrations = registrations,

    Splits = race.Splits?.Select(s => new SplitDTO
    {
      SplitId = s.SplitId,
      RaceId = s.RaceId,
      SplitName = s.SplitName,
      KmMark = s.KmMark,
    }).ToList(),

    TimeRecords = race.TimeRecords?.Select(t => new TimeRecordResponseDTO
    {
      TimeRecordId = t.TimeRecordId,
      ChipId = t.ChipId,
      RaceId = t.RaceId,
      SplitId = t.SplitId,
      SplitName = t.Split?.SplitName,
      KmMark = t.Split?.KmMark,
      Timestamp = t.Timestamp
    }).ToList()
  };
}
  }
}