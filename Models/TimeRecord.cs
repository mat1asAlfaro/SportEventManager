using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class TimeRecord
    {
        public int TimeRecordId { get; set; }
        public int ChipId { get; set; }
        public int RaceId { get; set; }
        public int SplitId { get; set; }
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