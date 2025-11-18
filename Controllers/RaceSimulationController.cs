using System.Threading.Tasks;
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
    public async Task<IActionResult> StartRace(int splitId)
    {
      await _raceRepository.NotifyRaceStartedAsync(splitId);
      return Ok($"Carrera {splitId} iniciada");
    }

    [HttpPost("update/{splitId}/{bibNumber}/{distanceKm}")]
    public async Task<IActionResult> UpdateRace(int splitId, int bibNumber, double distanceKm)
    {
      Console.WriteLine($"[CONTROLLER] Recibida actualización: Race={splitId}");

      // Notifica a través del servicio - esto actualiza automáticamente todos los componentes suscritos
      await _raceUpdateService.NotifyUpdate(splitId);

      return Ok(new { message = "Actualizacion enviada", splitId });
    }
  }
}
