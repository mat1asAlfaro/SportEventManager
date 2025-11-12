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
    public class EventRepository : IEventRepository
    {
        private readonly IDbContextFactory<SportEventDbContext> _contextFactory;

        public EventRepository(IDbContextFactory<SportEventDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            await using var _context = _contextFactory.CreateDbContext();
            return await Task.FromResult(_context.Events.ToList());
        }

        public async Task<Event?> GetEventByIdAsync(int eventId)
        {
            await using var _context = _contextFactory.CreateDbContext();
            var evnt = _context.Events.FirstOrDefault(e => e.EventId == eventId);
            return await Task.FromResult(evnt);
        }

        public async Task<List<EventDTO>> GetAllEventsWithRacesAndSplitsAsync()
        {
            await using var _context = _contextFactory.CreateDbContext();

            var events = await _context.Events
                .Include(e => e.Races)
                    .ThenInclude(r => r.RaceCategories!)
                        .ThenInclude(rc => rc.Category)
                .Include(e => e.Races)
                    .ThenInclude(r => r.Splits)
                .ToListAsync();

            return events.Select(e => new EventDTO
            {
                EventId = e.EventId,
                Name = e.Name,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location,
                TotalParticipantsRegistration = _context.Registrations
            .Where(r => e.Races.Select(ra => ra.RaceId).Contains(r.RaceId))
            .Select(r => r.ParticipantId)
            .Distinct()
            .Count(),
                Races = e.Races.Select(race => new RaceDTO
                {
                    RaceId = race.RaceId,
                    EventId = race.EventId,
                    RaceName = race.Name,
                    DistanceKm = race.DistanceKm,
                    MaxParticipants = race.MaxParticipants,
                    StartTime = race.StartTime,
                    TotalParticipantRegistration = _context.Registrations
                        .Where(reg => reg.RaceId == race.RaceId)
                        .Select(reg => reg.ParticipantId)
                        .Distinct()
                        .Count(),
                    RaceCategories = race?.RaceCategories?.Select(rc => new RaceCategoryDTO
                    {
                        RaceId = rc.RaceId,
                        CategoryId = rc.CategoryId,
                        Category = rc.Category == null ? null : new CategoryDTO
                        {
                            CategoryId = rc.Category.CategoryId,
                            ExternalName = rc.Category.ExternalName,
                            InternalName = rc.Category.InternalName,
                            Gender = rc.Category.Gender,
                            MinAge = rc.Category.MinAge,
                            MaxAge = rc.Category.MaxAge
                        }
                    }).ToList(),
                    Splits = race?.Splits?
                        .OrderBy(split => split.KmMark)
                        .Select(split => new SplitDTO
                        {
                            SplitId = split.SplitId,
                            SplitName = split.SplitName,
                            KmMark = split.KmMark
                        }).ToList()
                }).ToList()
            }).ToList();
        }

        public async Task AddEventAsync(Event evnt)
        {
            await using var _context = _contextFactory.CreateDbContext();
            _context.Events.Add(evnt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event evnt)
        {
            await using var _context = _contextFactory.CreateDbContext();
            _context.Events.Update(evnt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int eventId)
        {
            await using var _context = _contextFactory.CreateDbContext();
            var evnt = _context.Events.FirstOrDefault(e => e.EventId == eventId);
            if (evnt != null)
            {
                _context.Events.Remove(evnt);
                await _context.SaveChangesAsync();
            }
        }
    }
}