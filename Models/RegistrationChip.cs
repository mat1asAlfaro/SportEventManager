using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class RegistrationChip
    {
        [Key]
        public int RegistrationChipId { get; set; }
        [Required]
        public int RegistrationId { get; set; }
        public Registration? Registration { get; set; }
        [Required]
        public int ChipId { get; set; }
        public Chip? Chip { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public RegistrationChip()
        {
        }

        public RegistrationChip(int registrationChipId, int registrationId, int chipId, DateTime assignedAt)
        {
            RegistrationChipId = registrationChipId;
            RegistrationId = registrationId;
            ChipId = chipId;
            AssignedAt = DateTime.UtcNow;
        }
    }
}