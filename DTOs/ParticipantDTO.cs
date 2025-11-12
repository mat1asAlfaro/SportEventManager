using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class ParticipantDTO
    {
        public int ParticipantId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? DocumentNumber { get; set; }
        public ICollection<RegistrationDTO>? Registrations { get; set; }
    }
}