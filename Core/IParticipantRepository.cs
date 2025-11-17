using System.Collections.Generic;
using System.Threading.Tasks;
using SportEventManager.DTOs;
using SportEventManager.Models;

namespace SportEventManager.Core
{
    public interface IParticipantRepository
    {
        Task<List<Participant>> GetAllParticipantsAsync();
        Task<List<ParticipantDTO>> GetAllParticipantsDTOAsync();
        Task<Participant?> GetParticipantByIdAsync(int participantId);
        Task<Participant?> GetParticipantByDocumentAsync(string documentNumber);
        Task AddParticipantAsync(Participant participant);
        Task UpdateParticipantAsync(Participant participant);
        Task DeleteParticipantAsync(int participantId);
        Task<int?> GetParticipantAgeAsync(int participantId);
    }
}