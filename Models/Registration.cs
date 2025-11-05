using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; }
        [Required]
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }
        [Required]
        public int RaceId { get; set; }
        public Race? Race { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        [MaxLength(10)]
        public string? BibNumber { get; set; }

        [MaxLength(20)]
        public string? Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public ICollection<RegistrationChip>? RegistrationChips { get; set; }



        public Registration()
        {
        }

        public Registration(int registrationId, int participantId, int raceId, int categoryId, string? bibNumber, string? status)
        {
            RegistrationId = registrationId;
            ParticipantId = participantId;
            RaceId = raceId;
            CategoryId = categoryId;
            BibNumber = bibNumber;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }
    }
}