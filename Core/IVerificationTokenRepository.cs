using System.Threading.Tasks;
using SportEventManager.Models;

namespace SportEventManager.Core
{
    public interface IVerificationTokenRepository
    {
        Task InvalidateActiveAsync(string email, string purpose);
        Task AddAsync(VerificationToken token);
        Task<VerificationToken?> GetActiveAsync(string email, string purpose);
        Task ConsumeAsync(VerificationToken token);
    }
}
