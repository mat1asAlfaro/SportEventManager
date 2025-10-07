using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int RaceId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public Category()
        {
        }

        public Category(int categoryId, int raceId, string? name, string? gender, int minAge, int maxAge)
        {
            CategoryId = categoryId;
            RaceId = raceId;
            Name = name;
            Gender = gender;
            MinAge = minAge;
            MaxAge = maxAge;
        }
    }
}