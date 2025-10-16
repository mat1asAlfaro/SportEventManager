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
        private readonly SportEventDbContext _context;

        public EventRepository(SportEventDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await Task.FromResult(_context.Events.ToList());
        }

        public async Task<Event?> GetEventByIdAsync(int eventId)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.EventId == eventId);
            return await Task.FromResult(evnt);
        }

        public async Task<List<EventDTO>> GetEventsWithRacesAndParticipantCountAsync()
        {
            return await _context.Events
                .Select(e => new EventDTO
                {
                    EventId = e.EventId,
                    Name = e.Name,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Location = e.Location,

                    TotalParticipantsRegistration = _context.Registrations
                        .Where(r => _context.Races
                            .Where(ra => ra.EventId == e.EventId)
                            .Select(ra => ra.RaceId)
                            .Contains(r.RaceId))
                        .Select(r => r.ParticipantId)
                        .Distinct()
                        .Count(),

                    Races = _context.Races
                        .Where(race => race.EventId == e.EventId)
                        .Select(race => new RaceDTO
                        {
                            RaceId = race.RaceId,
                            Name = race.Name,
                            DistanceKm = race.DistanceKm,
                            MaxParticipants = race.MaxParticipants,
                            StartTime = race.StartTime,
                            TotalParticipantRegistration = _context.Registrations
                                .Where(reg => reg.RaceId == race.RaceId)
                                .Select(reg => reg.ParticipantId)
                                .Distinct()
                                .Count()
                        })
                        .ToList()

                })
                .ToListAsync();
        }

        public async Task AddEventAsync(Event evnt)
        {
            _context.Events.Add(evnt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event evnt)
        {
            var existingEvent = _context.Events.FirstOrDefault(e => e.EventId == evnt.EventId);
            if (existingEvent != null)
            {
                existingEvent.Name = evnt.Name;
                existingEvent.Description = evnt.Description;
                existingEvent.StartDate = evnt.StartDate;
                existingEvent.EndDate = evnt.EndDate;
                existingEvent.Location = evnt.Location;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEventAsync(int eventId)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.EventId == eventId);
            if (evnt != null)
            {
                _context.Events.Remove(evnt);
                await _context.SaveChangesAsync();
            }
        }
    }
}