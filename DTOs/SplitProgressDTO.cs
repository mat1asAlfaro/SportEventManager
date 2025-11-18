public class SplitProgressDTO
{
  public string? SplitName { get; set; }
  public double KmMark { get; set; }
  public bool Passed { get; set; }
  public DateTime? Timestamp { get; set; }
  public string TimestampFormatted => Timestamp?.ToString("HH:mm:ss") ?? "";
}
