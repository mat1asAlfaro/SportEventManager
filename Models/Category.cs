using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventManager.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }

        // Relationships
        public ICollection<RaceCategory>? RaceCategories { get; set; }
        public ICollection<Registration>? Registrations { get; set; }

        public Category()
        {
        }

        public Category(int categoryId, int raceId, string? name, string? gender, int minAge, int maxAge)
        {
            CategoryId = categoryId;
            Name = name;
            Gender = gender;
            MinAge = minAge;
            MaxAge = maxAge;
        }
    }
}