using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventManager.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(150)]
        public string? Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Race> Races { get; set; } = new List<Race>();

        public Event()
        {
        }

        public Event(int eventId, string name, string description, DateTime startDate, DateTime endDate, string location)
        {
            EventId = eventId;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            CreatedAt = DateTime.UtcNow;
        }
    }
}