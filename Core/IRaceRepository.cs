using SportEventManager.Models;

namespace SportEventManager.Core
{
  public interface IRaceRepository
  {
    Task<List<Race>> GetAllRacesAsync();
    Task<Race?> GetRaceByIdAsync(int raceId);
    Task AddRaceAsync(Race race);
    Task UpdateRaceAsync(Race race);
    Task DeleteRaceAsync(int raceId);
    Task<Race?> GetByIdAsync(int raceId); // Alias para compatibilidad
  }
}