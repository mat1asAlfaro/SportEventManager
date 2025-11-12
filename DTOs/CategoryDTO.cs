using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManager.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string? InternalName { get; set; }
        public string? ExternalName { get; set; }
        public string? Gender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public ICollection<RaceCategoryDTO>? RaceCategories { get; set; }
    }
}