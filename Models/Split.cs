using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Split
    {
        public int SplitId { get; set; }
        public int RaceId { get; set; }
        public string? SplitName { get; set; }
        public double KmMark { get; set; }

        public Split()
        {
        }

        public Split(int splitId, int raceId, string splitName, double kmMark)
        {
            SplitId = splitId;
            RaceId = raceId;
            SplitName = splitName;
            KmMark = kmMark;
        }
    }
}