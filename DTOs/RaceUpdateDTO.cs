namespace SportEventManager.DTOs;

public class RaceUpdateDTO
{
  public int RaceId { get; set; }
  public int BibNumber { get; set; }
  public double DistanceKm { get; set; }
  public DateTime Timestamp { get; set; }

  public RaceUpdateDTO() { }
  public RaceUpdateDTO(int bibnumber, double distanceKm)
  {
    BibNumber = bibnumber;
    DistanceKm = distanceKm;
    Timestamp = DateTime.UtcNow;
  }
}