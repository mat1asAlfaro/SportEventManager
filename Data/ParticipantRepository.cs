using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;

namespace SportEventManager.Data
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly SportEventDbContext _context;

        public ParticipantRepository(SportEventDbContext context)
        {
            _context = context;
        }

        public async Task<List<Participant>> GetAllParticipantsAsync()
        {
            return await _context.Participants
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }

        public async Task<Participant?> GetParticipantByIdAsync(int participantId)
        {
            return await _context.Participants
                .FirstOrDefaultAsync(p => p.ParticipantId == participantId);
        }

        public async Task<Participant?> GetParticipantByDocumentAsync(string documentNumber)
        {
            return await _context.Participants
                .FirstOrDefaultAsync(p => p.DocumentNumber == documentNumber);
        }

        public async Task AddParticipantAsync(Participant participant)
        {
            participant.CreatedAt = DateTime.UtcNow;
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParticipantAsync(Participant participant)
        {
            var existingParticipant = await _context.Participants
                .FirstOrDefaultAsync(p => p.ParticipantId == participant.ParticipantId);
            
            if (existingParticipant != null)
            {
                existingParticipant.FirstName = participant.FirstName;
                existingParticipant.LastName = participant.LastName;
                existingParticipant.Email = participant.Email;
                existingParticipant.DocumentNumber = participant.DocumentNumber;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteParticipantAsync(int participantId)
        {
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.ParticipantId == participantId);
            
            if (participant != null)
            {
                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync();
            }
        }
    }
}