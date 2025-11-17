using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;

namespace SportEventManager.Data
{
    public class VerificationTokenRepository : IVerificationTokenRepository
    {
        private readonly SportEventDbContext _db;

        public VerificationTokenRepository(SportEventDbContext db)
        {
            _db = db;
        }

        public async Task InvalidateActiveAsync(string email, string purpose)
        {
            var oldTokens = await _db.VerificationTokens
                .Where(t => t.Email == email && t.Purpose == purpose && !t.Consumed && t.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();

            foreach (var t in oldTokens)
            {
                t.ExpiresAt = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
        }

        public async Task AddAsync(VerificationToken token)
        {
            _db.VerificationTokens.Add(token);
            await _db.SaveChangesAsync();
        }

        public async Task<VerificationToken?> GetActiveAsync(string email, string purpose)
        {
            return await _db.VerificationTokens
                .Where(t => t.Email == email && t.Purpose == purpose && !t.Consumed && t.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task ConsumeAsync(VerificationToken token)
        {
            token.Consumed = true;
            token.ConsumedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}
