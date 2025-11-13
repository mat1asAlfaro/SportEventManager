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

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio.")]
        [MaxLength(8, ErrorMessage = "El documento no puede tener más de 8 caracteres.")]
        public string? DocumentNumber { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un género.")]
        public Gender? Gender { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Registration>? Registrations { get; set; }

        public Participant()
        {
        }

        public Participant(string firstName, string lastName, string email, string documentNumber, DateTime birthdate, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DocumentNumber = documentNumber;
            Birthdate = birthdate;
            Gender = gender;
            CreatedAt = DateTime.UtcNow;
        }
    }

    public enum Gender
    {
        [Display(Name = "Masculino")]
        Male = 1,

        [Display(Name = "Femenino")]
        Female = 2,

        [Display(Name = "Otro")]
        Other = 3
    }
}