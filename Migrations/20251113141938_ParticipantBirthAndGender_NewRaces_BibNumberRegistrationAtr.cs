using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantBirthAndGender_NewRaces_BibNumberRegistrationAtr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BibNumber",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Participants",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Participants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 2,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 3,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(2012, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 4,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(2008, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 5,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 6,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 7,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 8,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 9,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: 10,
                columns: new[] { "Birthdate", "Gender" },
                values: new object[] { new DateTime(1980, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "RaceCategories",
                columns: new[] { "CategoryId", "RaceId" },
                values: new object[] { 4, 1 });

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 1,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 2,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 3,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 4,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 5,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 6,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 7,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 8,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 9,
                column: "BibNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 10,
                column: "BibNumber",
                value: null);

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

            migrationBuilder.InsertData(
                table: "RaceCategories",
                columns: new[] { "CategoryId", "RaceId" },
                values: new object[,]
                {
                    { 3, 3 },
                    { 4, 3 },
                    { 3, 4 },
                    { 4, 4 },
                    { 3, 5 },
                    { 4, 5 },
                    { 3, 6 },
                    { 4, 6 },
                    { 3, 7 },
                    { 4, 7 },
                    { 3, 8 },
                    { 4, 8 },
                    { 5, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 5);

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

            migrationBuilder.DropColumn(
                name: "BibNumber",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Participants");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Participants",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);
        }
    }
}
