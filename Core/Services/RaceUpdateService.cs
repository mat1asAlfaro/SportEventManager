using SportEventManager.DTOs;

namespace SportEventManager.Core.Services
{
    /// <summary>
    /// Servicio para notificar actualizaciones de carreras en tiempo real.
    /// Los componentes Blazor se suscriben a este servicio para recibir actualizaciones.
    /// </summary>
    public class RaceUpdateService
    {
        // Evento que se dispara cuando hay una actualización
        public event Action<RaceUpdateDTO>? OnRaceUpdate;

        /// <summary>
        /// Notifica a todos los suscriptores sobre una actualización de carrera.
        /// </summary>
        public void NotifyUpdate(int raceId, int bibNumber, double distanceKm)
        {
            var update = new RaceUpdateDTO(bibNumber, distanceKm) 
            { 
                RaceId = raceId,
                Timestamp = DateTime.UtcNow 
            };
            
            Console.WriteLine($"[RaceUpdateService] Notificando actualización: Race={raceId}, Bib={bibNumber}, Dist={distanceKm}");
            OnRaceUpdate?.Invoke(update);
        }
    }
}
