using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Server.Migrations
{
    public partial class SeedingFillerData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Address", "Date", "Name", "Summary", "Type", "Urgency" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", null, "Oxygen", "Urgent" });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Address", "Date", "Name", "Summary", "Type", "Urgency" },
                values: new object[] { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Doe", null, "Bed", "Moderate" });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Address", "Date", "Name", "Summary", "Type", "Urgency" },
                values: new object[] { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jim Smith", null, "Food", "Not Urgent" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
