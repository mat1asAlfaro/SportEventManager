using System.Threading.Tasks;

namespace SportEventManager.Core.Hubs
{
    /// <summary>
    /// Interfaz fuertemente tipada para los métodos que el servidor invoca en los clientes de la carrera.
    /// </summary>
    public interface IRaceClient
    {
        Task ReceiveRaceUpdate(int raceId, int bibNumber, double distanceKm);
    }
}
