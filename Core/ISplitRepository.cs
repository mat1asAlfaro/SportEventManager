using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManager.DTOs;
using SportEventManager.Models;

namespace SportEventManager.Core
{
    public interface ISplitRepository
    {
        Task<List<Split>> GetAllSplitsAsync();
        Task<SplitDTO?> GetByIdAsync(int splitId);
        Task AddSplitAsync(Split split);
        Task UpdateSplitAsync(Split split);
        Task DeleteSplitAsync(int splitId);
        Task<IEnumerable<Split>> GetByRaceIdAsync(int raceId); // Obtener splits de una carrera
    }
}