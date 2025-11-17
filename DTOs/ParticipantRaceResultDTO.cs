using System;
using System.Collections.Generic;

namespace SportEventManager.DTOs
{
    public class ParticipantRaceResultDTO
    {
        public string ParticipantName { get; set; } = string.Empty;
        public int BibNumber { get; set; }
        public string RaceName { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public double DistanceKm { get; set; }
        
        // Tiempo total
        public TimeSpan? TotalTime { get; set; }
        public string TotalTimeFormatted => TotalTime?.ToString(@"hh\:mm\:ss") ?? "--:--:--";
        
        // Ritmo promedio (min/km)
        public TimeSpan? AveragePace { get; set; }
        public string AveragePaceFormatted => AveragePace?.ToString(@"mm\:ss") ?? "--:--";
        
        // Posiciones
        public int? OverallPosition { get; set; }
        public int? CategoryPosition { get; set; }
        public int TotalParticipants { get; set; }
        public int TotalInCategory { get; set; }
        
        // Estado
        public string Status { get; set; } = "No iniciado";
        
        // Tiempos parciales por split
        public List<SplitTimeDTO> SplitTimes { get; set; } = new();
    }

    public class SplitTimeDTO
    {
        public int SplitId { get; set; }
        public string SplitName { get; set; } = string.Empty;
        public double? KmMark { get; set; }
        public DateTime? Timestamp { get; set; }
        public string TimestampFormatted => Timestamp?.ToString("HH:mm:ss") ?? "--:--:--";
        public TimeSpan? TimeFromStart { get; set; }
        public string TimeFromStartFormatted => TimeFromStart?.ToString(@"hh\:mm\:ss") ?? "--:--:--";
        public TimeSpan? SplitTime { get; set; }
        public string SplitTimeFormatted => SplitTime?.ToString(@"hh\:mm\:ss") ?? "--:--:--";
        public int? PositionAtSplit { get; set; }
    }
}
