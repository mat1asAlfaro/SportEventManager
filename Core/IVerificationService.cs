using System.Threading.Tasks;

namespace SportEventManager.Core
{
    public interface IVerificationService
    {
        Task SendOtpAsync(string email, string purpose, int? participantId = null);
        Task<bool> ValidateOtpAsync(string email, string purpose, string code);
    }
}