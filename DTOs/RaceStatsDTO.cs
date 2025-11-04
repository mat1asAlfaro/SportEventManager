using System.Collections.Generic;

namespace SportEventManager.DTOs
{
    public class RaceStatsDTO
    {
        public int RaceId { get; set; }
        public string? RaceName { get; set; }
        public int TotalParticipants { get; set; }
        public int ParticipantsStarted { get; set; }
        public int ParticipantsFinished { get; set; }
        public Dictionary<int, int> ParticipantsBySplit { get; set; } = new();
    }
}