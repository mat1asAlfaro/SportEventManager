using System;

namespace SportEventManager.DTOs
{
    public class LiveTimeUpdateDTO
    {
        public int RaceId { get; set; }
        public int SplitId { get; set; }
        public string? SplitName { get; set; }
        public double? KmMark { get; set; }
        public string? ParticipantName { get; set; }
        public string? ChipSerialNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public int Position { get; set; } // Posición en ese split
        public TimeSpan? TimeFromStart { get; set; }
    }
}