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

        public async Task<bool> AssignAllRaceBibsAsync(int raceId)
        {
            var race = await _context.Races.FirstOrDefaultAsync(r => r.RaceId == raceId);
            if (race == null)
                throw new Exception("Carrera no encontrada.");

            var unassignedRegistrations = await _context.Registrations
                .Where(r => r.RaceId == raceId && r.BibNumber == null)
                .OrderBy(r => r.RegistrationId)
                .ToListAsync();

            if (!unassignedRegistrations.Any())
                return false;

            int lastBib = await _context.Registrations
                .Where(r => r.RaceId == raceId && r.BibNumber != null)
                .OrderByDescending(r => r.BibNumber)
                .Select(r => r.BibNumber!.Value)
                .FirstOrDefaultAsync();

            int nextBib = lastBib + 1;

            int maxAvailable = race.MaxParticipants - lastBib;
            if (unassignedRegistrations.Count > maxAvailable)
                throw new Exception("No hay suficientes dorsales disponibles para asignar a todos los participantes.");

            foreach (var registration in unassignedRegistrations)
            {
                registration.BibNumber = nextBib++;
            }

            await _context.SaveChangesAsync();

            return true;
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

        public async Task<bool> AssignBibNumberAsync(int registrationId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                var registration = await _context.Registrations.FindAsync(registrationId);
                if (registration == null)
                    throw new Exception("Registro no encontrado.");

                var race = await _context.Races.FindAsync(registration.RaceId);
                if (race == null)
                    throw new Exception("Carrera no encontrada.");

                var lastBib = await _context.Registrations
                    .Where(r => r.RaceId == registration.RaceId)
                    .OrderByDescending(r => r.BibNumber)
                    .Select(r => r.BibNumber)
                    .FirstOrDefaultAsync();

                int nextBib = (lastBib ?? 0) + 1;

                if (nextBib > race.MaxParticipants)
                    throw new Exception("La carrera ya alcanzó el máximo de inscripciones.");

                // Verificar duplicado justo antes de guardar
                bool exists = await _context.Registrations
                    .AnyAsync(r => r.RaceId == registration.RaceId && r.BibNumber == nextBib && r.RegistrationId != registrationId);

                if (exists)
                    throw new Exception($"El dorsal {nextBib} ya está asignado en esta carrera.");

                registration.BibNumber = nextBib;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            throw new Exception("No se pudo asignar un dorsal único tras varios intentos.");
        }

        public async Task<bool> RemoveBibNumberAsync(int registrationId)
        {
            var registration = await _context.Registrations.FindAsync(registrationId);
            if (registration == null)
                throw new Exception("Registro no encontrado.");

            registration.BibNumber = null;

            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> GetBibNumberAsync(int registrationId)
        {
            var registration = await _context.Registrations.FindAsync(registrationId);
            if (registration == null)
                throw new Exception("Registro no encontrado.");

            if (registration.BibNumber == null)
                throw new Exception("Este participante no tiene dorsal asignado.");

            return registration.BibNumber;
        }
    }
}