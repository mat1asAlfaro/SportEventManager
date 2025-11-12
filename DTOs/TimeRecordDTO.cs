namespace SportEventManager.DTOs
{
  public class TimeRecordDTO
  {
    public int TimeRecordId { get; set; }
    public int ChipId { get; set; }
    public ChipDTO? Chip { get; set; }
    public int RaceId { get; set; }
    public RaceDTO? Race { get; set; }
    public int SplitId { get; set; }
    public SplitDTO? Split { get; set; }
    public DateTime Timestamp { get; set; }
  }
}