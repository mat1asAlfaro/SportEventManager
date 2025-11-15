using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManager.Models;

namespace SportEventManager.DTOs
{
    public class SplitDTO
    {
        public int SplitId { get; set; }
        public int RaceId { get; set; }
        public RaceDTO? Race { get; set; }
        public string? SplitName { get; set; }
        public double? KmMark { get; set; }
    }
}