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
        [ForeignKey("EventId")]
        public Event? Event { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public double DistanceKm { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }

        // Relationships
        public ICollection<RaceCategory> RaceCategories { get; set; } = new List<RaceCategory>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<Split> Splits { get; set; } = new List<Split>();
        public ICollection<TimeRecord> TimeRecords { get; set; } = new List<TimeRecord>();

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