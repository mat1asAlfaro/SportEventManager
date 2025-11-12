 using System;

namespace SportEventManager.DTOs
{
    public class TimeRecordResponseDTO
    {
        public int TimeRecordId { get; set; }
        public int ChipId { get; set; }
        public string? ChipSerialNumber { get; set; }
        public int RaceId { get; set; }
        public string? RaceName { get; set; }
        public int SplitId { get; set; }
        public string? SplitName { get; set; }
        public double? KmMark { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ParticipantName { get; set; }
        public int? RegistrationId { get; set; }
    }
}