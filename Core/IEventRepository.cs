using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManager.Models;

namespace SportEventManager.Core
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int eventId);
        Task AddEventAsync(Event evnt);
        Task UpdateEventAsync(Event evnt);
        Task DeleteEventAsync(int eventId);
    }
}