using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class AtributteBibNumberRegistration : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
