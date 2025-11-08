 using System;

namespace SportEventManager.DTOs
{
    public class ChipReadingDTO
    {
        public int ChipId { get; set; }
        public int SplitId { get; set; }
        public DateTime? Timestamp { get; set; } // Opcional, si no se usa DateTime.UtcNow
    }
}