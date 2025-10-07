using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Chip
    {
        public int ChipId { get; set; }
        public string? SerialNumber { get; set; }

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