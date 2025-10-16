using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Split
    {
        [Key]
        public int SplitId { get; set; }
        [Required]
        public int RaceId { get; set; }
        public Race? Race { get; set; }
        [MaxLength(100)]
        public string? SplitName { get; set; }
        public double? KmMark { get; set; }

        // Relationships
        public ICollection<TimeRecord>? TimeRecord { get; set; }

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