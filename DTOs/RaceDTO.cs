using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class RaceDTO
    {
        public int RaceId { get; set; }
        public int EventId { get; set; }
        public string? Name { get; set; }
        public double DistanceKm { get; set; }
        public int TotalParticipantRegistration { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }
        public ICollection<RaceCategoryDTO>? RaceCategories { get; set; }
        // public ICollection<RegistrationDTO>? Registrations { get; set; }
        public ICollection<SplitDTO>? Splits { get; set; }
        // public ICollection<TimeRecordDTO>? TimeRecords { get; set; }
    }
}