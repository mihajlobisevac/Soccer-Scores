using Microsoft.EntityFrameworkCore.Migrations;

namespace Scores.Database.Migrations
{
    public partial class MatchStandingsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StandingsId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandingsId",
                table: "Matches");
        }
    }
}
