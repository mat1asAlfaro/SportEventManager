using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class SeedRaceCategoryMappings10k5kAnd2k : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RaceCategories",
                columns: new[] { "CategoryId", "RaceId" },
                values: new object[,]
                {
                    { 4, 1 },
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
        }
    }
}
