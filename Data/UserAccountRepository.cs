using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core;
using SportEventManager.Models;
using SportEventManager.Data.Persistence;

namespace SportEventManager.Data
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly SportEventDbContext _context;

        public UserAccountRepository(SportEventDbContext context)
        {
            _context = context;
        }

        public async Task<UserAccount?> GetByUsernameAsync(string username)
        {
            return await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
