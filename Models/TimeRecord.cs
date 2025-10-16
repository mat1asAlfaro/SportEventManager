using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class TimeRecord
    {
        [Key]
        public int TimeRecordId { get; set; }
        [Required]
        public int ChipId { get; set; }
        public Chip? Chip { get; set; }
        [Required]
        public int RaceId { get; set; }
        public Race? Race { get; set; }
        [Required]
        public int SplitId { get; set; }
        public Split? Split { get; set; }

        public DateTime Timestamp { get; set; }

        public TimeRecord()
        {
        }

        public TimeRecord(int timeRecordId, int chipId, int raceId, int splitId, DateTime timestamp)
        {
            TimeRecordId = timeRecordId;
            ChipId = chipId;
            RaceId = raceId;
            SplitId = splitId;
            Timestamp = timestamp;
        }
    }
}