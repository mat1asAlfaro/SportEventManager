using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SportEventManager.DTOs;

namespace SportEventManager.Core.Hubs
{
    [AllowAnonymous]
    public class RaceHub : Hub<IRaceClient>
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"[HUB] Cliente conectado: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"[HUB] Cliente desconectado: {Context.ConnectionId} - exc: {exception?.Message}");
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// El cliente agrega su conexión al grupo de la carrera específica.
        /// </summary>
        public async Task JoinRace(int raceId)
        {
            var group = GroupName(raceId);
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            Console.WriteLine($"[HUB] Conexión {Context.ConnectionId} unida a grupo {group}");
        }

        /// <summary>
        /// Actualización de estado enviada por un cliente (o backend). Se distribuye sólo al grupo de la carrera.
        /// </summary>
        public async Task UpdateRaceStatus(int raceId, int bibNumber, double distanceKm)
        {
            Console.WriteLine($"[HUB] Actualizacion recibida: Race={raceId}, Bib={bibNumber}, Distancia={distanceKm}");
            await Clients.Group(GroupName(raceId)).ReceiveRaceUpdate(raceId, bibNumber, distanceKm);
        }

        private static string GroupName(int raceId) => $"race_{raceId}";
    }
}