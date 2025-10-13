using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime CreatedAt { get; set; }

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