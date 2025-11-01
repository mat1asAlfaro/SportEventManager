using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantDobAndGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Participants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Participants",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CreatedAt", "Description", "EndDate", "Location", "Name", "StartDate" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Una carrera en Piria", new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Recorre Maldonado 8va etapa", new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Última etapa de Recorre Maldonado. ", new DateTime(2026, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maldonado", "Recorre Maldonado 9na etapa", new DateTime(2025, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Primera carrera del año en Punta", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Punta del Este", "San Fernando 2026", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Media Maratón anual de Montevideo.", new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Montevideo", "Half Marathon", new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1990, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1992, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 3,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1988, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 4,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1995, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 5,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1985, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 6,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1994, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 7,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1989, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 8,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1993, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 9,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1987, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino" });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 10,
                columns: new[] { "DateOfBirth", "Gender" },
                values: new object[] { new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Femenino" });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "RaceId", "DistanceKm", "EventId", "MaxParticipants", "Name", "StartTime" },
                values: new object[,]
                {
                    { 3, 10.0, 2, 1000, "10K", new DateTime(2025, 11, 8, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5.0, 2, 1000, "5 k", new DateTime(2025, 11, 8, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 10.0, 3, 1000, "10k", new DateTime(2025, 11, 29, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 5.0, 3, 1000, "5k", new DateTime(2025, 11, 29, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 10.0, 4, 4500, "10k", new DateTime(2026, 1, 10, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 5.0, 4, 4500, "5k", new DateTime(2026, 1, 10, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 2.0, 4, 1000, "San Fernandito", new DateTime(2026, 1, 10, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 21.0, 5, 4500, "21k", new DateTime(2026, 8, 3, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Participants");
        }
    }
}
