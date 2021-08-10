using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class UpdateNotice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85a2bb87-6cf3-4402-94eb-4b7720db800c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9364d806-708c-4def-9257-805a1987ea9d");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Notices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Notices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d572dda-9ac5-4e04-957e-9c3a01e4f781", "4990f127-62dc-4f03-90a5-1bc4bd55a76c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "028061d1-efe2-43f5-99c4-a66e58a74afa", "2b2cd0a9-591e-4053-9cb6-451d89cb0303", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "028061d1-efe2-43f5-99c4-a66e58a74afa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d572dda-9ac5-4e04-957e-9c3a01e4f781");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Notices");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85a2bb87-6cf3-4402-94eb-4b7720db800c", "8a80e5e6-8dbc-41de-9008-79d6da89bfb2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9364d806-708c-4def-9257-805a1987ea9d", "58e8b0c0-d75d-4b5a-a83c-db8803ad3712", "Administrator", "ADMINISTRATOR" });
        }
    }
}
