using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Chip
    {
        [Key]
        public int ChipId { get; set; }
        [MaxLength(50)]
        public string? SerialNumber { get; set; }

        // Relationships
        public ICollection<RegistrationChip>? RegistrationChip { get; set; }
        public ICollection<TimeRecord>? TimeRecord { get; set; }

        public Chip()
        {
        }

        public Chip(int chipId, string? serialNumber)
        {
            ChipId = chipId;
            SerialNumber = serialNumber;
        }
    }
}