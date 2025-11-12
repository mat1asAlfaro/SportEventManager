namespace SportEventManager.DTOs
{
    public class LiveParticipantDataDTO
    {
        public string ParticipantName { get; set; } = string.Empty;
        public int BibNumber { get; set; }
        public string RaceName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        
        // Tiempo transcurrido
        public TimeSpan? ElapsedTime { get; set; }
        public string ElapsedTimeFormatted => ElapsedTime?.ToString(@"hh\:mm\:ss") ?? "--:--:--";
        
        // Ritmo promedio (min/km)
        public TimeSpan? AveragePace { get; set; }
        public string AveragePaceFormatted => AveragePace?.ToString(@"mm\:ss") ?? "--:--";
        
        // Distancia recorrida
        public double DistanceCompleted { get; set; } // en km
        public string DistanceCompletedFormatted => $"{DistanceCompleted:F2} km";
        
        // Estado
        public string Status { get; set; } = "En Carrera"; // "En Carrera", "Finalizado", "No iniciado"
    }
}