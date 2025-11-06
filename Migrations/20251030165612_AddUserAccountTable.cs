using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            // Insertar usuarios admin
            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Username", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { "admin1", "admin1pass", "Admin" },
                    { "admin2", "admin2pass", "Admin" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
