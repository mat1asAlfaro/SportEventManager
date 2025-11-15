using System;
using System.ComponentModel.DataAnnotations;

namespace SportEventManager.Models
{
    public class VerificationToken
    {
        [Key]
        public int VerificationTokenId { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        public int? ParticipantId { get; set; }

        // Ej: "Registration:{EventId}:{RaceId}"
        [Required, MaxLength(100)]
        public string Purpose { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string CodeHash { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Salt { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Consumed { get; set; }
        public DateTime? ConsumedAt { get; set; }
    }
}