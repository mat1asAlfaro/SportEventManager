using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Models;

namespace SportEventManager.Data.Persistence
{
    public class SportEventDbContext : DbContext
    {
        public SportEventDbContext()
        {
        }

        public SportEventDbContext(DbContextOptions<SportEventDbContext> options)
            : base(options)
        {
        }

        // Definiciones de tablas
        public DbSet<Event> Events { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Chip> Chips { get; set; }
        public DbSet<RegistrationChip> RegistrationChips { get; set; }
        public DbSet<Split> Splits { get; set; }
        public DbSet<TimeRecord> TimeRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Event
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    Name = "Maratón Internacional",
                    Description = "Una carrera de larga distancia a nivel internacional.",
                    StartDate = new DateTime(2025, 3, 10, 8, 0, 0),
                    EndDate = new DateTime(2025, 3, 10, 12, 0, 0),
                    Location = "Ciudad Central",
                    MaxParticipants = 5000
                },
                new Event
                {
                    EventId = 2,
                    Name = "Carrera de 5K",
                    Description = "Competencia rápida en la ciudad.",
                    StartDate = new DateTime(2025, 4, 5, 7, 0, 0),
                    EndDate = new DateTime(2025, 4, 5, 9, 0, 0),
                    Location = "Parque Nacional",
                    MaxParticipants = 1000
                }
            );

            // Seed data for Race
            modelBuilder.Entity<Race>().HasData(
                new Race
                {
                    RaceId = 1,
                    EventId = 1,
                    Name = "Maratón 42K",
                    DistanceKm = 42,
                    StartTime = new DateTime(2025, 3, 10, 8, 0, 0)
                },
                new Race
                {
                    RaceId = 2,
                    EventId = 2,
                    Name = "Carrera 5K",
                    DistanceKm = 5,
                    StartTime = new DateTime(2025, 4, 5, 7, 0, 0)
                }
            );

            // Seed data for Category
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    RaceId = 1,
                    Name = "Absoluto",
                    Gender = "Ambos",
                    MinAge = 18,
                    MaxAge = 99
                },
                new Category
                {
                    CategoryId = 2,
                    RaceId = 2,
                    Name = "Femenino",
                    Gender = "Femenino",
                    MinAge = 18,
                    MaxAge = 99
                }
            );

            // Seed data for Participant
            modelBuilder.Entity<Participant>().HasData(
                new Participant
                {
                    ParticipantId = 1,
                    FirstName = "Juan",
                    LastName = "Pérez",
                    Email = "juan.perez@email.com",
                    DocumentNumber = "12345678"
                },
                new Participant
                {
                    ParticipantId = 2,
                    FirstName = "Ana",
                    LastName = "Gómez",
                    Email = "ana.gomez@email.com",
                    DocumentNumber = "87654321"
                }
            );

            // Seed data for Registration
            modelBuilder.Entity<Registration>().HasData(
                new Registration
                {
                    RegistrationId = 1,
                    ParticipantId = 1,
                    RaceId = 1,
                    CategoryId = 1,
                    Status = "Pending"
                },
                new Registration
                {
                    RegistrationId = 2,
                    ParticipantId = 2,
                    RaceId = 2,
                    CategoryId = 2,
                    Status = "Confirmed"
                }
            );

            // Seed data for Chip
            modelBuilder.Entity<Chip>().HasData(
                new Chip
                {
                    ChipId = 1,
                    SerialNumber = "CHIP1234"
                },
                new Chip
                {
                    ChipId = 2,
                    SerialNumber = "CHIP5678"
                }
            );

            // Seed data for RegistrationChip
            modelBuilder.Entity<RegistrationChip>().HasData(
                new RegistrationChip
                {
                    RegistrationChipId = 1,
                    RegistrationId = 1,
                    ChipId = 1,
                    AssignedAt = new DateTime(2025, 3, 10, 7, 30, 0)
                },
                new RegistrationChip
                {
                    RegistrationChipId = 2,
                    RegistrationId = 2,
                    ChipId = 2,
                    AssignedAt = new DateTime(2025, 4, 5, 6, 30, 0)
                }
            );

            // Seed data for Split
            modelBuilder.Entity<Split>().HasData(
                new Split
                {
                    SplitId = 1,
                    RaceId = 1,
                    SplitName = "Primer Kilómetro",
                    KmMark = 1
                },
                new Split
                {
                    SplitId = 2,
                    RaceId = 2,
                    SplitName = "Primer Kilómetro",
                    KmMark = 1
                }
            );

            // Seed data for TimeRecord
            modelBuilder.Entity<TimeRecord>().HasData(
                new TimeRecord
                {
                    TimeRecordId = 1,
                    ChipId = 1,
                    RaceId = 1,
                    SplitId = 1,
                    Timestamp = new DateTime(2025, 3, 10, 8, 5, 0)
                },
                new TimeRecord
                {
                    TimeRecordId = 2,
                    ChipId = 2,
                    RaceId = 2,
                    SplitId = 2,
                    Timestamp = new DateTime(2025, 4, 5, 7, 10, 0)
                }
            );
        }
    }
}