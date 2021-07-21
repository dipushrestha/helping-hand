using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class AddUniqueKeysForUrgencyAndServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "825f6155-9984-4b91-aa28-a328cce4ee9f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a2ec549-b9bc-4687-878d-0a43fc6371c2");

            migrationBuilder.AlterColumn<string>(
                name: "Service",
                table: "HelpServices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "HelpServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecd7e5c9-59aa-43c4-8ccc-e2b6ef7f8ca0", "cdfbc406-0c89-497e-b1b7-32f7cdfb5a55", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c4b7f0ab-e1dd-4664-b206-8c5d82e8e0b5", "b7688ce7-5644-42b4-ad90-e93532144031", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Urgencies_Level",
                table: "Urgencies",
                column: "Level",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HelpServices_Service",
                table: "HelpServices",
                column: "Service",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Urgencies_Level",
                table: "Urgencies");

            migrationBuilder.DropIndex(
                name: "IX_HelpServices_Service",
                table: "HelpServices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4b7f0ab-e1dd-4664-b206-8c5d82e8e0b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecd7e5c9-59aa-43c4-8ccc-e2b6ef7f8ca0");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "HelpServices");

            migrationBuilder.AlterColumn<string>(
                name: "Service",
                table: "HelpServices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "825f6155-9984-4b91-aa28-a328cce4ee9f", "cd1e6549-45ac-4c59-b93b-4d67e47f4459", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a2ec549-b9bc-4687-878d-0a43fc6371c2", "30ac33c7-823c-48da-90a9-bd79051cd3a2", "Administrator", "ADMINISTRATOR" });
        }
    }
}
