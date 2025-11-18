using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SportEventManager.Core;
using SportEventManager.Models;

namespace SportEventManager.Core.Services
{
    public class OtpVerificationService : IVerificationService
    {
        private readonly IVerificationTokenRepository _tokens;
        private readonly IEmailSender _email;
        private readonly ILogger<OtpVerificationService> _logger;

        public OtpVerificationService(IVerificationTokenRepository tokens, IEmailSender email, ILogger<OtpVerificationService> logger)
        {
            _tokens = tokens;
            _email = email;
            _logger = logger;
        }

        public async Task SendOtpAsync(string email, string purpose, int? participantId = null)
        {
            await _tokens.InvalidateActiveAsync(email, purpose);

            var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString(); // 6 d�gitos
            var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            var hash = Hash(code, salt);

            var token = new VerificationToken
            {
                Email = email,
                ParticipantId = participantId,
                Purpose = purpose,
                Salt = salt,
                CodeHash = hash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                CreatedAt = DateTime.UtcNow,
                Consumed = false
            };

            await _tokens.AddAsync(token);

            await _email.SendAsync(
                email,
                "C�digo de verificaci�n",
                $"Tu c�digo de verificaci�n es: {code}. Vence en 10 minutos.");

            _logger.LogInformation("OTP generado para {Email}, prop�sito {Purpose}", email, purpose);
        }

        public async Task<bool> ValidateOtpAsync(string email, string purpose, string code)
        {
            var token = await _tokens.GetActiveAsync(email, purpose);
            if (token is null) return false;

            var hash = Hash(code, token.Salt);
            var ok = hash == token.CodeHash;
            if (!ok) return false;

            await _tokens.ConsumeAsync(token);
            return true;
        }

        private static string Hash(string code, string salt)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(code + salt);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}