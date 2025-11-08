using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBibNumberToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BibNumber",
                table: "Registrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

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
            migrationBuilder.AlterColumn<string>(
                name: "BibNumber",
                table: "Registrations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
