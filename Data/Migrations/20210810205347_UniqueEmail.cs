using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class UniqueEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce2721d8-bdb5-4e2f-a170-510da7a5c0e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d228bba0-8835-4a1d-bdad-2aaeec16c5ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d436973f-14d4-447e-96b5-1307586eb55f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f04b51db-bde5-4d9c-9361-343ed780995e", "3e3b860d-aca0-477e-a89d-cd4bd76dd694", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c32a0f1-10b6-478e-ba69-9824b6765a04", "55101915-c3e5-44e9-933b-d23c18688608", "Announcer", "ANNOUNCER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eaa4c884-6261-452c-a811-9a42e4e23c2d", "24682f6d-50b9-4af1-abed-9df305643dfa", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c32a0f1-10b6-478e-ba69-9824b6765a04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eaa4c884-6261-452c-a811-9a42e4e23c2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f04b51db-bde5-4d9c-9361-343ed780995e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce2721d8-bdb5-4e2f-a170-510da7a5c0e8", "135909c3-6014-420b-ba31-f51536b8114c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d436973f-14d4-447e-96b5-1307586eb55f", "10d895a8-dbf2-48de-8358-ee21f722c97d", "Announcer", "ANNOUNCER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d228bba0-8835-4a1d-bdad-2aaeec16c5ca", "70700151-174c-46ce-a3ed-8a510e32334d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
