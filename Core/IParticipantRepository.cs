using System.Collections.Generic;
using System.Threading.Tasks;
using SportEventManager.Models;

namespace SportEventManager.Core
{
    public interface IParticipantRepository
    {
        Task<List<Participant>> GetAllParticipantsAsync();
        Task<Participant?> GetParticipantByIdAsync(int participantId);
        Task<Participant?> GetParticipantByDocumentAsync(string documentNumber);
        Task AddParticipantAsync(Participant participant);
        Task UpdateParticipantAsync(Participant participant);
        Task DeleteParticipantAsync(int participantId);
    }
}