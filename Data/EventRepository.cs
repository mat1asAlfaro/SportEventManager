using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;

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