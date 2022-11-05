using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserStory4Tours.Migrations
{
    public partial class ST : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToursDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    direction = table.Column<int>(nullable: false),
                    DateDeparture = table.Column<DateTime>(nullable: false),
                    NumberNight = table.Column<int>(nullable: false),
                    CostVac = table.Column<decimal>(nullable: false),
                    NumberVac = table.Column<int>(nullable: false),
                    Wi_Fi = table.Column<bool>(nullable: false),
                    Surcharges = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToursDB", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToursDB");
        }
    }
}
