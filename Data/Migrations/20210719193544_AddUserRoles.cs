using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class AddUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "825f6155-9984-4b91-aa28-a328cce4ee9f", "cd1e6549-45ac-4c59-b93b-4d67e47f4459", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a2ec549-b9bc-4687-878d-0a43fc6371c2", "30ac33c7-823c-48da-90a9-bd79051cd3a2", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "825f6155-9984-4b91-aa28-a328cce4ee9f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a2ec549-b9bc-4687-878d-0a43fc6371c2");
        }
    }
}
