using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class RaceCategory
    {
        public int RaceId { get; set; }
        public Race? Race { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public RaceCategory()
        {
        }

        public RaceCategory(int raceId, int categoryId)
        {
            RaceId = raceId;
            CategoryId = categoryId;
        }
    }
}