using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class UpdateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "HelpRequests",
                newName: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "HelpRequests",
                newName: "Summary");
        }
    }
}
