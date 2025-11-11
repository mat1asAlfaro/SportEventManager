using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core;
using SportEventManager.Core.Services;

namespace SportEventManager.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RaceSimulationController : ControllerBase
  {
    private readonly IRaceRepository _raceRepository;
    private readonly RaceUpdateService _raceUpdateService;

    public RaceSimulationController(IRaceRepository raceRepository, RaceUpdateService raceUpdateService)
    {
      _raceRepository = raceRepository;
      _raceUpdateService = raceUpdateService;
    }

    [HttpPost("start/{raceId}")]
    public async Task<IActionResult> StartRace(int raceId)
    {
      await _raceRepository.NotifyRaceStartedAsync(raceId);
      return Ok($"Carrera {raceId} iniciada");
    }

    [HttpPost("update/{raceId}/{bibNumber}/{distanceKm}")]
    public IActionResult UpdateRace(int raceId, int bibNumber, double distanceKm)
    {
      Console.WriteLine($"[CONTROLLER] Recibida actualización: Race={raceId}, Dorsal={bibNumber}, Distancia={distanceKm}");
      
      // Notifica a través del servicio - esto actualiza automáticamente todos los componentes suscritos
      _raceUpdateService.NotifyUpdate(raceId, bibNumber, distanceKm);
      
      return Ok(new { message = "Actualizacion enviada", raceId, bibNumber, distanceKm });
    }
  }
}
