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
        public DbSet<RaceCategory> RaceCategories { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Chip> Chips { get; set; }
        public DbSet<RegistrationChip> RegistrationChips { get; set; }
        public DbSet<Split> Splits { get; set; }
        public DbSet<TimeRecord> TimeRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // RaceCategory (Many-to-Many)
            modelBuilder.Entity<RaceCategory>()
                .HasKey(rc => new { rc.RaceId, rc.CategoryId });

            modelBuilder.Entity<RaceCategory>()
                .HasOne(rc => rc.Race)
                .WithMany(r => r.RaceCategories)
                .HasForeignKey(rc => rc.RaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RaceCategory>()
                .HasOne(rc => rc.Category)
                .WithMany(c => c.RaceCategories)
                .HasForeignKey(rc => rc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Registration relationships
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Participant)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Race)
                .WithMany(race => race.Registrations)
                .HasForeignKey(r => r.RaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // RegistrationChip relationships
            modelBuilder.Entity<RegistrationChip>()
                .HasOne(rc => rc.Registration)
                .WithMany(r => r.RegistrationChips)
                .HasForeignKey(rc => rc.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RegistrationChip>()
                .HasOne(rc => rc.Chip)
                .WithMany(c => c.RegistrationChip)
                .HasForeignKey(rc => rc.ChipId)
                .OnDelete(DeleteBehavior.Cascade);

            // Split relationships
            modelBuilder.Entity<Split>()
                .HasOne(s => s.Race)
                .WithMany(r => r.Splits)
                .HasForeignKey(s => s.RaceId)
                .OnDelete(DeleteBehavior.Cascade);

            // TimeRecord relationships
            modelBuilder.Entity<TimeRecord>()
                .HasOne(tr => tr.Chip)
                .WithMany(c => c.TimeRecord)
                .HasForeignKey(tr => tr.ChipId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeRecord>()
                .HasOne(tr => tr.Race)
                .WithMany(r => r.TimeRecords)
                .HasForeignKey(tr => tr.RaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeRecord>()
                .HasOne(tr => tr.Split)
                .WithMany(s => s.TimeRecord)
                .HasForeignKey(tr => tr.SplitId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes opcionales
            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Name);

            modelBuilder.Entity<Race>()
                .HasIndex(r => r.Name);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Participant>()
                .HasIndex(p => new { p.LastName, p.FirstName });

            modelBuilder.Entity<Participant>()
                .HasIndex(p => p.Email)
                .IsUnique();

            // Seed Data
            modelBuilder.Entity<Event>().HasData(
                new Event { EventId = 1, Name = "Maratón Anual", Description = "Maratón de 10K y 5K", StartDate = new DateTime(2025, 12, 1), EndDate = new DateTime(2025, 12, 2), Location = "Montevideo", CreatedAt = new DateTime(2025, 10, 15) }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race { RaceId = 1, EventId = 1, Name = "10K Adultos", DistanceKm = 10, MaxParticipants = 100, StartTime = new DateTime(2025, 12, 1, 09, 00, 00) },
                new Race { RaceId = 2, EventId = 1, Name = "5K Junior", DistanceKm = 5, MaxParticipants = 80, StartTime = new DateTime(2025, 12, 1, 08, 00, 00) }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Adultos Masculino", Gender = "M", MinAge = 18, MaxAge = 40 },
                new Category { CategoryId = 2, Name = "Adultos Femenino", Gender = "F", MinAge = 18, MaxAge = 40 },
                new Category { CategoryId = 3, Name = "Junior Masculino", Gender = "M", MinAge = 14, MaxAge = 17 },
                new Category { CategoryId = 4, Name = "Junior Femenino", Gender = "F", MinAge = 14, MaxAge = 17 }
            );

            modelBuilder.Entity<RaceCategory>().HasData(
                new RaceCategory { RaceId = 1, CategoryId = 1 },
                new RaceCategory { RaceId = 1, CategoryId = 2 },
                new RaceCategory { RaceId = 2, CategoryId = 3 },
                new RaceCategory { RaceId = 2, CategoryId = 4 }
            );

            modelBuilder.Entity<Participant>().HasData(
                new Participant { ParticipantId = 1, FirstName = "Matias", LastName = "Alfaro", Email = "matias@test.com", DocumentNumber = "12345678", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 2, FirstName = "Ana", LastName = "Gomez", Email = "ana@test.com", DocumentNumber = "87654321", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 3, FirstName = "Lucas", LastName = "Perez", Email = "lucas@test.com", DocumentNumber = "11223344", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 4, FirstName = "Sofia", LastName = "Martinez", Email = "sofia@test.com", DocumentNumber = "44332211", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 5, FirstName = "Juan", LastName = "Diaz", Email = "juan@test.com", DocumentNumber = "55667788", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 6, FirstName = "Lucia", LastName = "Fernandez", Email = "lucia@test.com", DocumentNumber = "99887766", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 7, FirstName = "Carlos", LastName = "Rojas", Email = "carlos@test.com", DocumentNumber = "66778899", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 8, FirstName = "Laura", LastName = "Vazquez", Email = "laura@test.com", DocumentNumber = "77889900", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 9, FirstName = "Pedro", LastName = "Torres", Email = "pedro@test.com", DocumentNumber = "33445566", CreatedAt = new DateTime(2025, 10, 15) },
                new Participant { ParticipantId = 10, FirstName = "Mariana", LastName = "Suarez", Email = "mariana@test.com", DocumentNumber = "22334455", CreatedAt = new DateTime(2025, 10, 15) }
            );

            modelBuilder.Entity<Registration>().HasData(
                new Registration { RegistrationId = 1, ParticipantId = 1, RaceId = 1, CategoryId = 1, Status = "Confirmed", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 2, ParticipantId = 2, RaceId = 1, CategoryId = 2, Status = "Confirmed", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 3, ParticipantId = 3, RaceId = 2, CategoryId = 3, Status = "Pending", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 4, ParticipantId = 4, RaceId = 2, CategoryId = 4, Status = "Pending", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 5, ParticipantId = 5, RaceId = 1, CategoryId = 1, Status = "Confirmed", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 6, ParticipantId = 6, RaceId = 1, CategoryId = 2, Status = "Confirmed", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 7, ParticipantId = 7, RaceId = 2, CategoryId = 3, Status = "Pending", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 8, ParticipantId = 8, RaceId = 2, CategoryId = 4, Status = "Pending", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 9, ParticipantId = 9, RaceId = 1, CategoryId = 1, Status = "Confirmed", CreatedAt = new DateTime(2025, 10, 15) },
                new Registration { RegistrationId = 10, ParticipantId = 10, RaceId = 1, CategoryId = 2, Status = "Confirmed", CreatedAt = new DateTime(2025, 10, 15) }
            );

            modelBuilder.Entity<Chip>().HasData(
                new Chip { ChipId = 1, SerialNumber = "CHIP-001" },
                new Chip { ChipId = 2, SerialNumber = "CHIP-002" },
                new Chip { ChipId = 3, SerialNumber = "CHIP-003" },
                new Chip { ChipId = 4, SerialNumber = "CHIP-004" },
                new Chip { ChipId = 5, SerialNumber = "CHIP-005" },
                new Chip { ChipId = 6, SerialNumber = "CHIP-006" },
                new Chip { ChipId = 7, SerialNumber = "CHIP-007" },
                new Chip { ChipId = 8, SerialNumber = "CHIP-008" },
                new Chip { ChipId = 9, SerialNumber = "CHIP-009" },
                new Chip { ChipId = 10, SerialNumber = "CHIP-010" }
            );

            modelBuilder.Entity<RegistrationChip>().HasData(
                new RegistrationChip { RegistrationChipId = 1, RegistrationId = 1, ChipId = 1, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 2, RegistrationId = 2, ChipId = 2, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 3, RegistrationId = 3, ChipId = 3, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 4, RegistrationId = 4, ChipId = 4, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 5, RegistrationId = 5, ChipId = 5, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 6, RegistrationId = 6, ChipId = 6, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 7, RegistrationId = 7, ChipId = 7, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 8, RegistrationId = 8, ChipId = 8, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 9, RegistrationId = 9, ChipId = 9, AssignedAt = new DateTime(2025, 10, 15) },
                new RegistrationChip { RegistrationChipId = 10, RegistrationId = 10, ChipId = 10, AssignedAt = new DateTime(2025, 10, 15) }
            );

            modelBuilder.Entity<Split>().HasData(
                new Split { SplitId = 1, RaceId = 1, SplitName = "Km 5", KmMark = 5 },
                new Split { SplitId = 2, RaceId = 1, SplitName = "Km 10", KmMark = 10 },
                new Split { SplitId = 3, RaceId = 2, SplitName = "Km 2.5", KmMark = 2.5 },
                new Split { SplitId = 4, RaceId = 2, SplitName = "Km 5", KmMark = 5 }
            );

            modelBuilder.Entity<TimeRecord>().HasData(
                new TimeRecord { TimeRecordId = 1, ChipId = 1, RaceId = 1, SplitId = 1, Timestamp = new DateTime(2025, 10, 15, 08, 05, 00) },
                new TimeRecord { TimeRecordId = 2, ChipId = 2, RaceId = 1, SplitId = 2, Timestamp = new DateTime(2025, 10, 15, 08, 10, 00) },
                new TimeRecord { TimeRecordId = 3, ChipId = 3, RaceId = 2, SplitId = 3, Timestamp = new DateTime(2025, 10, 15, 08, 15, 00) },
                new TimeRecord { TimeRecordId = 4, ChipId = 4, RaceId = 2, SplitId = 4, Timestamp = new DateTime(2025, 10, 15, 08, 20, 00) }
            );
        }
    }
}