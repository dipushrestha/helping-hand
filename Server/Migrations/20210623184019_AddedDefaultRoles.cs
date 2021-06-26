using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Server.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb21a80a-72a0-42d1-b6a6-0282c50dd3c9", "33c2e0df-b0c9-4da6-a355-6df968889255", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a104687b-fc95-40a7-ab42-08979250cac2", "381e103e-628d-49b9-8c69-17db6f1b4919", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a104687b-fc95-40a7-ab42-08979250cac2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb21a80a-72a0-42d1-b6a6-0282c50dd3c9");
        }
    }
}
