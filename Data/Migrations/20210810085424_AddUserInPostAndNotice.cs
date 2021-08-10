using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class AddUserInPostAndNotice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HelpRequests",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Notices_UserId",
                table: "Notices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_UserId",
                table: "HelpRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_AspNetUsers_UserId",
                table: "HelpRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_AspNetUsers_UserId",
                table: "Notices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_AspNetUsers_UserId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_AspNetUsers_UserId",
                table: "Notices");

            migrationBuilder.DropIndex(
                name: "IX_Notices_UserId",
                table: "Notices");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_UserId",
                table: "HelpRequests");

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

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HelpRequests");

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
    }
}
