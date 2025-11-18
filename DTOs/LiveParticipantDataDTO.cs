namespace SportEventManager.DTOs
{
    public class LiveParticipantDataDTO
    {
        public string ParticipantName { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public int BibNumber { get; set; }
        public string RaceName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        // Posición Actual
        public int CurrentPosition { get; set; }

        // Tiempo transcurrido
        public TimeSpan? ElapsedTime { get; set; }
        public string ElapsedTimeFormatted => ElapsedTime?.ToString(@"hh\:mm\:ss") ?? "--:--:--";

        // Ritmo promedio
        public TimeSpan? AveragePace { get; set; }
        public string AveragePaceFormatted => AveragePace?.ToString(@"mm\:ss") ?? "--:--";

        // Ritmo Actual
        public TimeSpan? CurrentPace { get; set; }
        public string CurrentPaceFormatted => CurrentPace?.ToString(@"mm\:ss") ?? "--:--";

        // Distancia recorrida
        public double DistanceCompleted { get; set; }
        public string DistanceCompletedFormatted => $"{DistanceCompleted:F2} km";

        // Porcentaje según distancia recorrida
        public double ProgressPercentage { get; set; }
        public string ProgressPercentageFormatted => $"{ProgressPercentage:F1}%";

        // Porcentaje según splits completados
        public double SplitsPercentage { get; set; }
        public string SplitsPercentageFormatted => $"{SplitsPercentage:F1}%";

        // Lista de puntos de control
        public List<SplitProgressDTO> SplitsProgress { get; set; } = new();
        public SplitProgressDTO? LastPassedSplit =>
            SplitsProgress
                .Where(sp => sp.Passed)
                .OrderByDescending(sp => sp.KmMark)
                .FirstOrDefault();


        // Tiempo estimado de llegada
        public TimeSpan? EstimatedTimeToFinish { get; set; }
        public string EstimatedTimeToFinishFormatted => EstimatedTimeToFinish?.ToString(@"hh\:mm\:ss") ?? "--:--:--";

        public DateTime? EstimatedFinishDateTime { get; set; }
        public string EstimatedFinishDateTimeFormatted => EstimatedFinishDateTime?.ToString("HH:mm:ss") ?? "--:--:--";

        // Distancia restante
        public double DistanceLeft { get; set; }

        // Distancia interpolada
        public double InterpolatedDistance { get; set; }
        public double ProgressPercentageInterpolated { get; set; }
        public string ProgressPercentageInterpolateFormatted => $"{ProgressPercentageInterpolated:0.00}%";

        // Estado
        public string Status { get; set; } = "En Carrera";
    }
}