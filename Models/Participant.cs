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

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression("^(Masculino|Femenino)$", ErrorMessage = "El género debe ser Masculino o Femenino.")]
        public string? Gender { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Registration>? Registrations { get; set; }

        public Participant()
        {
        }

        public Participant(string firstName, string lastName, string email, string documentNumber, DateTime dateOfBirth, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DocumentNumber = documentNumber;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            CreatedAt = DateTime.UtcNow;
        }
    }
}