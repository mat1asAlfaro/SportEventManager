using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public int TotalParticipantsRegistration { get; set; }
        public List<RaceDTO> Races { get; set; } = new();
    }
}