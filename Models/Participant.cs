using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(8)]
        public string? DocumentNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Registration>? Registrations { get; set; }

        public Participant()
        {
        }

        public Participant(string firstName, string lastName, string email, string documentNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DocumentNumber = documentNumber;
            CreatedAt = DateTime.UtcNow;
        }
    }
}