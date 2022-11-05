using Microsoft.EntityFrameworkCore.Migrations;

namespace UserStory4Tours.Migrations
{
    public partial class AddMigrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MigrationVerification",
                table: "ToursDB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MigrationVerification",
                table: "ToursDB");
        }
    }
}
