using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class RegistrationDTO
    {
        public int RegistrationId { get; set; }
        public int ParticipantId { get; set; }
        public int RaceId { get; set; }
        public int EventId { get; set; }
        public string? RaceName { get; set; }
        public string? EventName { get; set; }
        public int CategoryId { get; set; }
        public string? Status { get; set; }
        public string? ParticipantName { get; set; }
        public int? BibNumber { get; set; }
        public ICollection<RegistrationChipDTO> RegistrationChips { get; set; } = new List<RegistrationChipDTO>();
    }
}