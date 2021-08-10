using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class AddNotice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4b7f0ab-e1dd-4664-b206-8c5d82e8e0b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecd7e5c9-59aa-43c4-8ccc-e2b6ef7f8ca0");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Urgencies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85a2bb87-6cf3-4402-94eb-4b7720db800c", "8a80e5e6-8dbc-41de-9008-79d6da89bfb2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9364d806-708c-4def-9257-805a1987ea9d", "58e8b0c0-d75d-4b5a-a83c-db8803ad3712", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Urgencies_Label",
                table: "Urgencies",
                column: "Label",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropIndex(
                name: "IX_Urgencies_Label",
                table: "Urgencies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85a2bb87-6cf3-4402-94eb-4b7720db800c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9364d806-708c-4def-9257-805a1987ea9d");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Urgencies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecd7e5c9-59aa-43c4-8ccc-e2b6ef7f8ca0", "cdfbc406-0c89-497e-b1b7-32f7cdfb5a55", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c4b7f0ab-e1dd-4664-b206-8c5d82e8e0b5", "b7688ce7-5644-42b4-ad90-e93532144031", "Administrator", "ADMINISTRATOR" });
        }
    }
}
