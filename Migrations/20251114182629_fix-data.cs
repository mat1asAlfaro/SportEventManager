using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Migrations
{
    /// <inheritdoc />
    public partial class FixData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 1,
                columns: new[] { "KmMark", "SplitName" },
                values: new object[] { 0.0, "Km 0" });

            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 2,
                columns: new[] { "KmMark", "SplitName" },
                values: new object[] { 5.0, "Km 5" });

            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 3,
                columns: new[] { "KmMark", "RaceId", "SplitName" },
                values: new object[] { 10.0, 1, "Km 10" });

            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 4,
                columns: new[] { "KmMark", "SplitName" },
                values: new object[] { 0.0, "Km 0" });

            migrationBuilder.InsertData(
                table: "Splits",
                columns: new[] { "SplitId", "KmMark", "RaceId", "SplitName" },
                values: new object[,]
                {
                    { 5, 2.5, 2, "Km 2.5" },
                    { 6, 5.0, 2, "Km 5" }
                });

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2025, 10, 15, 8, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 2,
                columns: new[] { "ChipId", "Timestamp" },
                values: new object[] { 1, new DateTime(2025, 10, 15, 8, 20, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 3,
                columns: new[] { "ChipId", "RaceId", "Timestamp" },
                values: new object[] { 1, 1, new DateTime(2025, 10, 15, 8, 40, 40, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 4,
                columns: new[] { "ChipId", "Timestamp" },
                values: new object[] { 3, new DateTime(2025, 10, 15, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TimeRecords",
                columns: new[] { "TimeRecordId", "ChipId", "RaceId", "SplitId", "Timestamp" },
                values: new object[,]
                {
                    { 5, 3, 2, 5, new DateTime(2025, 10, 15, 8, 4, 30, 0, DateTimeKind.Unspecified) },
                    { 6, 3, 2, 6, new DateTime(2025, 10, 15, 8, 8, 40, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 1,
                columns: new[] { "KmMark", "SplitName" },
                values: new object[] { 5.0, "Km 5" });

            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 2,
                columns: new[] { "KmMark", "SplitName" },
                values: new object[] { 10.0, "Km 10" });

            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 3,
                columns: new[] { "KmMark", "RaceId", "SplitName" },
                values: new object[] { 2.5, 2, "Km 2.5" });

            migrationBuilder.UpdateData(
                table: "Splits",
                keyColumn: "SplitId",
                keyValue: 4,
                columns: new[] { "KmMark", "SplitName" },
                values: new object[] { 5.0, "Km 5" });

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2025, 10, 15, 8, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 2,
                columns: new[] { "ChipId", "Timestamp" },
                values: new object[] { 2, new DateTime(2025, 10, 15, 8, 10, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 3,
                columns: new[] { "ChipId", "RaceId", "Timestamp" },
                values: new object[] { 3, 2, new DateTime(2025, 10, 15, 8, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TimeRecords",
                keyColumn: "TimeRecordId",
                keyValue: 4,
                columns: new[] { "ChipId", "Timestamp" },
                values: new object[] { 4, new DateTime(2025, 10, 15, 8, 20, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
