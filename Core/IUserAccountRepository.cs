
namespace SportEventManager.Core
{
    public interface IUserAccountRepository
    {
        Task<UserAccount?> GetByUsernameAsync(string username);
    }
}