using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "RaceCategories",
                columns: new[] { "CategoryId", "RaceId" },
                values: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 1,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 2,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 5,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 6,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 9,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 10,
                column: "CategoryId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RaceCategories",
                keyColumns: new[] { "CategoryId", "RaceId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.InsertData(
                table: "RaceCategories",
                columns: new[] { "CategoryId", "RaceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 2,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 5,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 6,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 9,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 10,
                column: "CategoryId",
                value: 2);
        }
    }
}
