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
        public string? InternalName { get; set; }
        [MaxLength(100)]
        public string? ExternalName { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        [NotMapped]
        public bool? IsSelected { get; set; }

        // Relationships
        public ICollection<RaceCategory>? RaceCategories { get; set; }
        public ICollection<Registration>? Registrations { get; set; }

        public Category()
        {
        }

        public Category(int categoryId, string? internalName, string? externalName, string? gender, int minAge, int maxAge)
        {
            CategoryId = categoryId;
            InternalName = internalName;
            ExternalName = externalName;
            Gender = gender;
            MinAge = minAge;
            MaxAge = maxAge;
        }
    }
}