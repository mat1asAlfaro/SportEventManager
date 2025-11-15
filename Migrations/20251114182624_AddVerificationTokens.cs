using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerificationTokens",
                columns: table => new
                {
                    VerificationTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Consumed = table.Column<bool>(type: "bit", nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationTokens", x => x.VerificationTokenId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationTokens_Email_Purpose_Consumed_ExpiresAt",
                table: "VerificationTokens",
                columns: new[] { "Email", "Purpose", "Consumed", "ExpiresAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationTokens");
        }
    }
}
