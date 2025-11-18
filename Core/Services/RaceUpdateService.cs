using SportEventManager.DTOs;

namespace SportEventManager.Core.Services
{
    /// <summary>
    /// Servicio para notificar actualizaciones de carreras en tiempo real.
    /// Los componentes Blazor se suscriben a este servicio para recibir actualizaciones.
    /// </summary>
    public class RaceUpdateService
    {
        private readonly IServiceProvider _serviceProvider;

        public event Action<int>? OnRaceUpdate;

        public RaceUpdateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Notifica a todos los suscriptores sobre una actualización de carrera.
        /// </summary>
        public async Task NotifyUpdate(int splitId)
        {
            using var scope = _serviceProvider.CreateScope();
            var splitRepo = scope.ServiceProvider.GetRequiredService<ISplitRepository>();

            var split = await splitRepo.GetByIdAsync(splitId);

            if (split == null)
            {
                Console.WriteLine($"[RaceUpdateService] Split {splitId} no existe");
                return;
            }

            if (split.Race == null)
            {
                Console.WriteLine($"[RaceUpdateService] Split {splitId} no tiene Race cargada");
                return;
            }

            var raceId = split.Race.RaceId;

            Console.WriteLine($"[RaceUpdateService] Notificando actualización Race={raceId}");
            OnRaceUpdate?.Invoke(raceId);
        }
    }
}
