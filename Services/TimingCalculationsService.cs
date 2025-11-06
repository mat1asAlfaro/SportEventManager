using System;
using System.Collections.Generic;
using System.Linq;
using SportEventManager.Models;

namespace SportEventManager.Services
{
    /// Interfaz para cálculos de tiempos, pace y velocidades
    public interface ITimingCalculationsService
    {
        string CalculatePace(TimeSpan time, double distanceKm);
        double CalculateSpeed(TimeSpan time, double distanceKm);
        TimeSpan CalculateSplitTime(DateTime currentTime, DateTime previousTime);
        int CalculatePosition(List<TimeRecord> records, int targetRecordId);
    }

    /// Servicio para realizar cálculos relacionados con timing de carreras
    public class TimingCalculationsService : ITimingCalculationsService
    {
        /// Calcula el pace (ritmo) en formato "min:seg/km"
        /// Ejemplo: 5 km en 26:15 = 5:15 min/km
        public string CalculatePace(TimeSpan time, double distanceKm)
        {
            if (distanceKm <= 0) return "N/A";
            
            var totalMinutes = time.TotalMinutes;
            var paceMinutes = totalMinutes / distanceKm;
            
            var minutes = (int)paceMinutes;
            var seconds = (int)((paceMinutes - minutes) * 60);
            
            return $"{minutes}:{seconds:D2} min/km";
        }

        /// Calcula la velocidad en km/h
        /// Ejemplo: 10 km en 1 hora = 10 km/h
        public double CalculateSpeed(TimeSpan time, double distanceKm)
        {
            if (time.TotalHours <= 0) return 0;
            
            return Math.Round(distanceKm / time.TotalHours, 2);
        }
        /// Calcula el tiempo de un segmento (diferencia entre dos timestamps)
        public TimeSpan CalculateSplitTime(DateTime currentTime, DateTime previousTime)
        {
            return currentTime - previousTime;
        }

        /// Calcula la posición de un corredor basándose en el orden de llegada (timestamp)
        public int CalculatePosition(List<TimeRecord> records, int targetRecordId)
        {
            var orderedRecords = records.OrderBy(r => r.Timestamp).ToList();
            var position = orderedRecords.FindIndex(r => r.TimeRecordId == targetRecordId);
            return position >= 0 ? position + 1 : 0;
        }
    }
}