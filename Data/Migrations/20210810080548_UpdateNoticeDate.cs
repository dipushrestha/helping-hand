using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class UpdateNoticeDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "028061d1-efe2-43f5-99c4-a66e58a74afa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d572dda-9ac5-4e04-957e-9c3a01e4f781");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Notices",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2b16a24-8101-4658-8a73-99ac4e646425", "1e64ee99-5862-4d70-ab2a-d469f11cdd55", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79f73471-05f3-4b50-86fc-a8d8d7b7b2e8", "3dd67c18-0709-46f4-b016-9898a622059b", "Announcer", "ANNOUNCER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a154ccfa-f9e6-4877-920d-ed3cd48d4af4", "c1357508-a05d-46ba-ab65-bc4b68b2ee8d", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79f73471-05f3-4b50-86fc-a8d8d7b7b2e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a154ccfa-f9e6-4877-920d-ed3cd48d4af4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2b16a24-8101-4658-8a73-99ac4e646425");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Notices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d572dda-9ac5-4e04-957e-9c3a01e4f781", "4990f127-62dc-4f03-90a5-1bc4bd55a76c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "028061d1-efe2-43f5-99c4-a66e58a74afa", "2b2cd0a9-591e-4053-9cb6-451d89cb0303", "Administrator", "ADMINISTRATOR" });
        }
    }
}
