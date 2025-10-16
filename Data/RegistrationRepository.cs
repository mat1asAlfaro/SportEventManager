using System;
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
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly SportEventDbContext _context;

        public RegistrationRepository(SportEventDbContext context)
        {
            _context = context;
        }

        public async Task<List<Registration>> GetAllRegistrationsAsync()
        {
            return await Task.FromResult(_context.Registrations.ToList());
        }

        public async Task<Registration?> GetRegistrationByIdAsync(int registrationId)
        {
            var registration = _context.Registrations.FirstOrDefault(r => r.RegistrationId == registrationId);
            return await Task.FromResult(registration);
        }

        public async Task AddRegistrationAsync(Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRegistrationAsync(Registration registration)
        {
            var existingRegistration = _context.Registrations.FirstOrDefault(r => r.RegistrationId == registration.RegistrationId);
            if (existingRegistration != null)
            {
                existingRegistration.Status = registration.Status;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRegistrationAsync(int registrationId)
        {
            var registration = _context.Registrations.FirstOrDefault(r => r.RegistrationId == registrationId);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }
    }
}