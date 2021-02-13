using Microsoft.EntityFrameworkCore.Migrations;

namespace Workspace_DAL.Migrations
{
    public partial class assignmentupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cancel",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancel",
                table: "Assignments");
        }
    }
}
