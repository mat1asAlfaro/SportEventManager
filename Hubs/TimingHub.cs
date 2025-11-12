using Microsoft.AspNetCore.SignalR;
using SportEventManager.DTOs;

namespace SportEventManager.Hubs
{
    // Hub de SignalR para comunicación en tiempo real del sistema de timing
    public class TimingHub : Hub
    {
        // Permite a un cliente suscribirse a actualizaciones de una carrera específica
        public async Task SubscribeToRace(int raceId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"race_{raceId}");
        }

        // Permite a un cliente cancelar la suscripción a una carrera
        public async Task UnsubscribeFromRace(int raceId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"race_{raceId}");
        }

        // Permite a un cliente suscribirse a un split/punto de control específico
        public async Task SubscribeToSplit(int splitId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"split_{splitId}");
        }

        // Permite a un cliente cancelar la suscripción a un split
        public async Task UnsubscribeFromSplit(int splitId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"split_{splitId}");
        }

        // Se ejecuta cuando un cliente se conecta
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        // Se ejecuta cuando un cliente se desconecta
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}