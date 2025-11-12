using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.DTOs;
using SportEventManager.Models;

namespace SportEventManager.Data
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly IDbContextFactory<SportEventDbContext> _contextFactory;

        public ParticipantRepository(IDbContextFactory<SportEventDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Participant>> GetAllParticipantsAsync()
        {
            await using var _context = _contextFactory.CreateDbContext();
            return await _context.Participants
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }

        public async Task<List<ParticipantDTO>> GetAllParticipantsDTOAsync()
        {
            await using var _context = _contextFactory.CreateDbContext();

            var participants = await _context.Participants
                .Select(p => new ParticipantDTO
                {
                    ParticipantId = p.ParticipantId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    DocumentNumber = p.DocumentNumber,
                    Registrations = p.Registrations!.Select(r => new RegistrationDTO
                    {
                        RegistrationId = r.RegistrationId,
                        ParticipantId = r.ParticipantId,
                        RaceId = r.RaceId,
                        EventId = _context.Races
                            .Where(race => race.RaceId == r.RaceId)
                            .Select(race => race.EventId)
                            .FirstOrDefault(),
                        RaceName = _context.Races
                            .Where(race => race.RaceId == r.RaceId)
                            .Select(race => race.Name)
                            .FirstOrDefault(),
                        EventName = _context.Races
                            .Where(race => race.RaceId == r.RaceId)
                            .Join(_context.Events, race => race.EventId, e => e.EventId, (race, e) => e.Name)
                            .FirstOrDefault(),
                        CategoryId = r.CategoryId,
                        Status = r.Status,
                        BibNumber = r.BibNumber,
                        RegistrationChips = r.RegistrationChips!.Select(rc => new RegistrationChipDTO
                        {
                            RegistrationChipId = rc.RegistrationChipId,
                            RegistrationId = rc.RegistrationId,
                            ChipId = rc.ChipId
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return participants;
        }


        public async Task<Participant?> GetParticipantByIdAsync(int participantId)
        {
            await using var _context = _contextFactory.CreateDbContext();
            return await _context.Participants
                .FirstOrDefaultAsync(p => p.ParticipantId == participantId);
        }

        public async Task<Participant?> GetParticipantByDocumentAsync(string documentNumber)
        {
            await using var _context = _contextFactory.CreateDbContext();
            return await _context.Participants
                .FirstOrDefaultAsync(p => p.DocumentNumber == documentNumber);
        }

        public async Task AddParticipantAsync(Participant participant)
        {
            await using var _context = _contextFactory.CreateDbContext();
            participant.CreatedAt = DateTime.UtcNow;
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParticipantAsync(Participant participant)
        {
            await using var _context = _contextFactory.CreateDbContext();
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
            await using var _context = _contextFactory.CreateDbContext();
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