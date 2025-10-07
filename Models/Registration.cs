using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int ParticipantId { get; set; }
        public int RaceId { get; set; }
        public int CategoryId { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Registration()
        {
        }

        public Registration(int registrationId, int participantId, int raceId, int categoryId, string? status)
        {
            RegistrationId = registrationId;
            ParticipantId = participantId;
            RaceId = raceId;
            CategoryId = categoryId;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }
    }
}