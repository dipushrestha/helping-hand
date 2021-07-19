using Microsoft.EntityFrameworkCore.Migrations;

namespace helpinghand.Data.Migrations
{
    public partial class StatusAddedInUrgency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Urgencies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Urgencies");
        }
    }
}
