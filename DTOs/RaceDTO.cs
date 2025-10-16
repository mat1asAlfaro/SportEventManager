using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class RaceDTO
    {
        public int RaceId { get; set; }
        public string? Name { get; set; }
        public double DistanceKm { get; set; }
        public int TotalParticipantRegistration { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }
    }
}