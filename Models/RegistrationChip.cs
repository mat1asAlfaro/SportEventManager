using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class RegistrationChip
    {
        public int RegistrationChipId { get; set; }
        public int RegistrationId { get; set; }
        public int ChipId { get; set; }
        public DateTime AssignedAt { get; set; }

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