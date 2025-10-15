using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventManager.Models
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public double DistanceKm { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }

        // Relationships
        public ICollection<RaceCategory>? RaceCategories { get; set; }
        public ICollection<Registration>? Registrations { get; set; }
        public ICollection<Split>? Splits { get; set; }
        public ICollection<TimeRecord>? TimeRecords { get; set; }

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