using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class ChipDTO
    {
        public int ChipId { get; set; }
        public string? SerialNumber { get; set; }
        // public ICollection<TimeRecordDTO>? TimeRecord { get; set; }
    }
}