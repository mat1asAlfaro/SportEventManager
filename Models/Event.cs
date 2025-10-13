using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace SportEventManager.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime CreatedAt { get; set; }

        public Event()
        {
        }

        public Event(int eventId, string name, string description, DateTime startDate, DateTime endDate, string location, int maxParticipants)
        {
            EventId = eventId;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            MaxParticipants = maxParticipants;
            CreatedAt = DateTime.UtcNow;
        }
    }
}