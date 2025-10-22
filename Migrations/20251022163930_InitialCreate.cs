using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MinAge = table.Column<int>(type: "int", nullable: true),
                    MaxAge = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Chips",
                columns: table => new
                {
                    ChipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chips", x => x.ChipId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DistanceKm = table.Column<double>(type: "float", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Races_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceCategories",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceCategories", x => new { x.RaceId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_RaceCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceCategories_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrations_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Splits",
                columns: table => new
                {
                    SplitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    SplitName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KmMark = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Splits", x => x.SplitId);
                    table.ForeignKey(
                        name: "FK_Splits_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationChips",
                columns: table => new
                {
                    RegistrationChipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    ChipId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationChips", x => x.RegistrationChipId);
                    table.ForeignKey(
                        name: "FK_RegistrationChips_Chips_ChipId",
                        column: x => x.ChipId,
                        principalTable: "Chips",
                        principalColumn: "ChipId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationChips_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeRecords",
                columns: table => new
                {
                    TimeRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChipId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    SplitId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRecords", x => x.TimeRecordId);
                    table.ForeignKey(
                        name: "FK_TimeRecords_Chips_ChipId",
                        column: x => x.ChipId,
                        principalTable: "Chips",
                        principalColumn: "ChipId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeRecords_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeRecords_Splits_SplitId",
                        column: x => x.SplitId,
                        principalTable: "Splits",
                        principalColumn: "SplitId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "TimeRecords",
                columns: new[] { "TimeRecordId", "ChipId", "RaceId", "SplitId", "Timestamp" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new DateTime(2025, 10, 15, 8, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 1, 2, new DateTime(2025, 10, 15, 8, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 2, 3, new DateTime(2025, 10, 15, 8, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 2, 4, new DateTime(2025, 10, 15, 8, 20, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Name",
                table: "Events",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_Email",
                table: "Participants",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_LastName_FirstName",
                table: "Participants",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_RaceCategories_CategoryId",
                table: "RaceCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_EventId",
                table: "Races",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_Name",
                table: "Races",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationChips_ChipId",
                table: "RegistrationChips",
                column: "ChipId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationChips_RegistrationId",
                table: "RegistrationChips",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_CategoryId",
                table: "Registrations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ParticipantId",
                table: "Registrations",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_RaceId",
                table: "Registrations",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Splits_RaceId",
                table: "Splits",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRecords_ChipId",
                table: "TimeRecords",
                column: "ChipId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRecords_RaceId",
                table: "TimeRecords",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRecords_SplitId",
                table: "TimeRecords",
                column: "SplitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceCategories");

            migrationBuilder.DropTable(
                name: "RegistrationChips");

            migrationBuilder.DropTable(
                name: "TimeRecords");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Chips");

            migrationBuilder.DropTable(
                name: "Splits");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
