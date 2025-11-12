using SportEventManager.Models;

namespace SportEventManager.Core
{
  public interface IRaceRepository
  {
    Task<List<Race>> GetAllRacesAsync();
    Task<List<Race>> GetRacesByEventIdAsync(int eventId);
    Task<Race?> GetRaceByIdAsync(int raceId);
    Task AddRaceAsync(Race race);
    Task UpdateRaceAsync(Race race);
    Task DeleteRaceAsync(int raceId);


    Task UpdateRaceStatusAsync(int raceId, int bibNumber, double distanceKm);
    Task NotifyRaceStartedAsync(int raceId);

  }
}