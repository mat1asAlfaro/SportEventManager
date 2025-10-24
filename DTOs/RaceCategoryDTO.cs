using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class RaceCategoryDTO
    {
        public int RaceId { get; set; }
        public RaceDTO? Race { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }
    }
}