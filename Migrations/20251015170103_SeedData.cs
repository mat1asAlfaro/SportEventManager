using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Gender", "MaxAge", "MinAge", "Name" },
                values: new object[,]
                {
                    { 1, "M", 40, 18, "Adultos Masculino" },
                    { 2, "F", 40, 18, "Adultos Femenino" },
                    { 3, "M", 17, 14, "Junior Masculino" },
                    { 4, "F", 17, 14, "Junior Femenino" }
                });

            migrationBuilder.InsertData(
                table: "Chips",
                columns: new[] { "ChipId", "SerialNumber" },
                values: new object[,]
                {
                    { 1, "CHIP-001" },
                    { 2, "CHIP-002" },
                    { 3, "CHIP-003" },
                    { 4, "CHIP-004" },
                    { 5, "CHIP-005" },
                    { 6, "CHIP-006" },
                    { 7, "CHIP-007" },
                    { 8, "CHIP-008" },
                    { 9, "CHIP-009" },
                    { 10, "CHIP-010" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CreatedAt", "Description", "EndDate", "Location", "Name", "StartDate" },
                values: new object[] { 1, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maratón de 10K y 5K", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Montevideo", "Maratón Anual", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ParticipantId", "CreatedAt", "DocumentNumber", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "12345678", "matias@test.com", "Matias", "Alfaro" },
                    { 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "87654321", "ana@test.com", "Ana", "Gomez" },
                    { 3, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "11223344", "lucas@test.com", "Lucas", "Perez" },
                    { 4, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "44332211", "sofia@test.com", "Sofia", "Martinez" },
                    { 5, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "55667788", "juan@test.com", "Juan", "Diaz" },
                    { 6, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "99887766", "lucia@test.com", "Lucia", "Fernandez" },
                    { 7, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "66778899", "carlos@test.com", "Carlos", "Rojas" },
                    { 8, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "77889900", "laura@test.com", "Laura", "Vazquez" },
                    { 9, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "33445566", "pedro@test.com", "Pedro", "Torres" },
                    { 10, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "22334455", "mariana@test.com", "Mariana", "Suarez" }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "RaceId", "DistanceKm", "EventId", "MaxParticipants", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, 10.0, 1, 100, "10K Adultos", new DateTime(2025, 12, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 5.0, 1, 80, "5K Junior", new DateTime(2025, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "RaceCategories",
                columns: new[] { "CategoryId", "RaceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "RegistrationId", "CategoryId", "CreatedAt", "ParticipantId", "RaceId", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Confirmed" },
                    { 2, 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Confirmed" },
                    { 3, 3, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Pending" },
                    { 4, 4, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "Pending" },
                    { 5, 1, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, "Confirmed" },
                    { 6, 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "Confirmed" },
                    { 7, 3, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, "Pending" },
                    { 8, 4, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, "Pending" },
                    { 9, 1, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1, "Confirmed" },
                    { 10, 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, "Confirmed" }
                });

            migrationBuilder.InsertData(
                table: "Splits",
                columns: new[] { "SplitId", "KmMark", "RaceId", "SplitName" },
                values: new object[,]
                {
                    { 1, 5.0, 1, "Km 5" },
                    { 2, 10.0, 1, "Km 10" },
                    { 3, 2.5, 2, "Km 2.5" },
                    { 4, 5.0, 2, "Km 5" }
                });

            migrationBuilder.InsertData(
                table: "RegistrationChips",
                columns: new[] { "RegistrationChipId", "AssignedAt", "ChipId", "RegistrationId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 4, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4 },
                    { 5, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5 },
                    { 6, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 6 },
                    { 7, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7 },
                    { 8, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 8 },
                    { 9, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 9 },
                    { 10, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RegistrationChips",
                keyColumn: "RegistrationChipId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Chips",
                keyColumn: "ChipId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);
        }
    }
}
