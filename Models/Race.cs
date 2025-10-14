using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Race
    {
        public int RaceId { get; set; }
        public int EventId { get; set; }
        public string? Name { get; set; }
        public double DistanceKm { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }

        public Race()
        {
        }

        public Race(int raceId, int eventId, string name, double distanceKm, int maxParticipants, DateTime startTime)
        {
            RaceId = raceId;
            EventId = eventId;
            Name = name;
            DistanceKm = distanceKm;
            MaxParticipants = maxParticipants;
            StartTime = startTime;
        }
    }
}