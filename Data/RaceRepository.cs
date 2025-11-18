using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Core.Hubs;
using SportEventManager.Models;
using SportEventManager.DTOs;

namespace SportEventManager.Data
{
  public class RaceRepository : IRaceRepository
  {
    private readonly IDbContextFactory<SportEventDbContext> _contextFactory;
    private IHubContext<RaceHub> _hubContext;

    public RaceRepository(IDbContextFactory<SportEventDbContext> contextFactory, IHubContext<RaceHub> hubContext)
    {
      _contextFactory = contextFactory;
      _hubContext = hubContext;
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

    public async Task<RaceDTO?> GetRaceDTOByIdWithTimeRecordDTOAsync(int raceId)
    {
      await using var _context = _contextFactory.CreateDbContext();

      var race = await _context.Races
          .Include(r => r.Event)
          .Include(r => r.RaceCategories)
              .ThenInclude(rc => rc.Category)
          .Include(r => r.Registrations)
              .ThenInclude(rg => rg.RegistrationChips)
          .Include(r => r.Splits)
          .Include(r => r.TimeRecords)
          .FirstOrDefaultAsync(r => r.RaceId == raceId);

      if (race == null)
        return null;

      return new RaceDTO
      {
        RaceId = race.RaceId,
        EventId = race.EventId,
        RaceName = race.Name,
        EventName = race.Event?.Name,
        DistanceKm = race.DistanceKm,
        MaxParticipants = race.MaxParticipants,
        StartTime = race.StartTime,
        TotalParticipantRegistration = race.Registrations?.Count ?? 0,

        Event = race.Event == null ? null : new EventDTO
        {
          EventId = race.Event.EventId,
          Name = race.Event.Name,
          Description = race.Event.Description,
          StartDate = race.Event.StartDate,
          EndDate = race.Event.EndDate,
          Location = race.Event.Location,
          TotalParticipantsRegistration = race.Event.Races.Sum(r => r.Registrations.Count())
        },

        RaceCategories = race.RaceCategories?.Select(rc => new RaceCategoryDTO
        {
          RaceId = rc.RaceId,
          CategoryId = rc.CategoryId,
          Category = rc.Category == null ? null : new CategoryDTO
          {
            CategoryId = rc.Category.CategoryId,
            InternalName = rc.Category.InternalName,
            ExternalName = rc.Category.ExternalName,
            Gender = rc.Category.Gender,
            MinAge = rc.Category.MinAge,
            MaxAge = rc.Category.MaxAge
          }
        }).ToList(),

        Registrations = race.Registrations?.Select(rg => new RegistrationDTO
        {
          RegistrationId = rg.RegistrationId,
          ParticipantId = rg.ParticipantId,
          RaceId = rg.RaceId,
          EventId = rg.Race?.Event?.EventId ?? 0,
          RaceName = rg.Race?.Name ?? string.Empty,
          EventName = rg.Race?.Event?.Name ?? string.Empty,
          CategoryId = rg.CategoryId,
          Status = rg.Status,
          BibNumber = rg.BibNumber,
          RegistrationChips = rg.RegistrationChips != null
                ? rg.RegistrationChips.Select(c => new RegistrationChipDTO
                {
                  RegistrationChipId = c.RegistrationChipId,
                  RegistrationId = c.RegistrationId,
                  ChipId = c.ChipId,
                }).ToList()
                : new List<RegistrationChipDTO>()
        }).ToList() ?? new List<RegistrationDTO>(),

        Splits = race.Splits?.Select(s => new SplitDTO
        {
          SplitId = s.SplitId,
          RaceId = s.RaceId,
          SplitName = s.SplitName,
          KmMark = s.KmMark,
        }).ToList(),

        TimeRecordsDTO = race.TimeRecords?.Select(t => new TimeRecordDTO
        {
          TimeRecordId = t.TimeRecordId,
          ChipId = t.ChipId,
          RaceId = t.RaceId,
          SplitId = t.SplitId,
          Timestamp = t.Timestamp,
        }).ToList()
      };
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

      var defaultSplits = new List<Split>
      {
        new Split { Race = race, SplitName = "Salida", KmMark = 0 },
        new Split { Race = race, SplitName = "Punto Medio", KmMark = race.DistanceKm / 2 },
        new Split { Race = race, SplitName = "Entrada a Meta", KmMark = race.DistanceKm - 0.05},
        new Split { Race = race, SplitName = "Meta", KmMark = race.DistanceKm }
      };

      foreach (var split in defaultSplits)
      {
        _context.Splits.Add(split);
      }

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

    public async Task<List<RaceDTO>> GetLiveRacing()
    {
      await using var _context = _contextFactory.CreateDbContext();
      var now = DateTime.UtcNow;

      var liveRaces = await _context.Races
          .Include(r => r.Event)
          .Include(r => r.RaceCategories).ThenInclude(rc => rc.Category)
          .Include(r => r.Registrations).ThenInclude(reg => reg.RegistrationChips)
          .Include(r => r.Splits)
          .Include(r => r.TimeRecords)
          .Where(r => r.StartTime <= now && r.Event!.EndDate >= now)
          .Select(r => new RaceDTO
          {
            RaceId = r.RaceId,
            EventId = r.EventId,
            EventName = r.Event!.Name,
            RaceName = r.Name,
            DistanceKm = r.DistanceKm,
            MaxParticipants = r.MaxParticipants,
            StartTime = r.StartTime,
            TotalParticipantRegistration = r.Registrations!.Count(),

            Event = r.Event != null ? new EventDTO
            {
              EventId = r.Event.EventId,
              Name = r.Event.Name,
              Description = r.Event.Description,
              StartDate = r.Event.StartDate,
              EndDate = r.Event.EndDate,
              Location = r.Event.Location,
            } : null,

            RaceCategories = r.RaceCategories!.Select(rc => new RaceCategoryDTO
            {
              RaceId = rc.RaceId,
              CategoryId = rc.CategoryId,
              Category = new CategoryDTO
              {
                CategoryId = rc.Category!.CategoryId,
                InternalName = rc.Category.InternalName,
                ExternalName = rc.Category.ExternalName,
                Gender = rc.Category.Gender,
                MinAge = rc.Category.MinAge,
                MaxAge = rc.Category.MaxAge,
              }
            }).ToList(),

            Registrations = r.Registrations!.Select(reg => new RegistrationDTO
            {
              RegistrationId = reg.RegistrationId,
              ParticipantId = reg.ParticipantId,
              RaceId = reg.RaceId,
              EventId = r.EventId,
              RaceName = r.Name,
              EventName = r.Event!.Name,
              CategoryId = reg.CategoryId,
              Status = reg.Status,
              BibNumber = reg.BibNumber,
              RegistrationChips = reg.RegistrationChips!.Select(ch => new RegistrationChipDTO
              {
                RegistrationChipId = ch.RegistrationChipId,
                ChipId = ch.ChipId,
                RegistrationId = ch.RegistrationId
              }).ToList()
            }).ToList(),

            Splits = r.Splits!.Select(s => new SplitDTO
            {
              SplitId = s.SplitId,
              RaceId = s.RaceId,
              SplitName = s.SplitName,
              KmMark = s.KmMark
            }).ToList(),

            TimeRecordsDTO = r.TimeRecords!.Select(t => new TimeRecordDTO
            {
              TimeRecordId = t.TimeRecordId,
              ChipId = t.ChipId,
              RaceId = t.RaceId,
              SplitId = t.SplitId,
              Timestamp = t.Timestamp,
              Chip = new ChipDTO
              {
                ChipId = t.Chip!.ChipId,
                SerialNumber = t.Chip.SerialNumber
              },
              Split = new SplitDTO
              {
                SplitId = t.Split!.SplitId,
                SplitName = t.Split.SplitName,
                KmMark = t.Split.KmMark
              }
            }).ToList()
          })
          .ToListAsync();

      return liveRaces;
    }


    public async Task UpdateRaceStatusAsync(int raceId, int bibNumber, double distanceKm)
    {
      await using var _context = _contextFactory.CreateDbContext();

      var race = await _context.Races.FindAsync(raceId);
      if (race == null) return;

      // Logica interna de actualizacion (opcional)
      await _context.SaveChangesAsync();

      // Emitir actualizacion en tiempo readonly
      Console.WriteLine($"[REPO] Emitiendo actualización para race_{raceId}: dorsal={bibNumber}, distancia={distanceKm}");
      // await _hubContext.Clients.All.SendAsync("ReceiveRaceUpdate", raceId, bibNumber, distanceKm);
      await _hubContext.Clients.Group(raceId.ToString()).SendAsync("ReceiveRaceUpdate", raceId, bibNumber, distanceKm);
    }

    public async Task NotifyRaceStartedAsync(int raceId)
    {
      await _hubContext.Clients.Group(raceId.ToString())
            .SendAsync("RaceStarted", new { Message = $"Carrera {raceId} iniciada" });
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