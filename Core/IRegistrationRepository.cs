using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManager.DTOs;
using SportEventManager.Models;

namespace SportEventManager.Core
{
    public interface IRegistrationRepository
    {
        Task<List<Registration>> GetAllRegistrationsAsync();
        Task<Registration?> GetRegistrationByIdAsync(int registrationId);
        Task AddRegistrationAsync(Registration registration);
        Task UpdateRegistrationAsync(Registration registration);
        Task DeleteRegistrationAsync(int registrationId);
        Task<bool> ExistsByParticipantAndEventAsync(int participantId, int eventId);
        Task<bool> ExistsByParticipantAndRaceAsync(int participantId, int raceId);
        Task<Registration?> GetParticipantRegistrationInEventAsync(int participantId, int eventId);
        Task<bool> AssignAllRaceBibsAsync(int raceId);
        Task<bool> AssignBibNumberAsync(int registrationId);
        Task<bool> RemoveBibNumberAsync(int registrationId);
        Task<int?> GetBibNumberAsync(int registrationId);
        Task<IEnumerable<Registration>> GetByRaceIdAsync(int raceId);
        Task<int> CountByRaceIdAsync(int raceId);
    }
}
