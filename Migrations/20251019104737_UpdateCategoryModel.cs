using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "InternalName");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                newName: "IX_Categories_InternalName");

            migrationBuilder.AddColumn<string>(
                name: "ExternalName",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "ExternalName", "InternalName", "MaxAge" },
                values: new object[] { "Caballeros Mayores (18 años o más)", "Caballeros - Mayores", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "ExternalName", "InternalName", "MaxAge" },
                values: new object[] { "Damas Mayores (18 años o más)", "Damas - Mayores", null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "ExternalName", "Gender", "InternalName", "MaxAge", "MinAge" },
                values: new object[] { "Mixtos Mayores", "X", "Mixtos - Mayores", null, 18 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "ExternalName", "Gender", "InternalName", "MinAge" },
                values: new object[] { "Juveniles (12 - 17 años)", "X", "Juveniles - Mixtos", 12 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "ExternalName", "Gender", "InternalName", "MaxAge", "MinAge" },
                values: new object[] { 5, "Infantiles (hasta 11 años)", "X", "Infantiles", 11, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ExternalName",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "InternalName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_InternalName",
                table: "Categories",
                newName: "IX_Categories_Name");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "MaxAge", "Name" },
                values: new object[] { 40, "Adultos Masculino" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "MaxAge", "Name" },
                values: new object[] { 40, "Adultos Femenino" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "Gender", "MaxAge", "MinAge", "Name" },
                values: new object[] { "M", 17, 14, "Junior Masculino" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "Gender", "MinAge", "Name" },
                values: new object[] { "F", 14, "Junior Femenino" });
        }
    }
}
