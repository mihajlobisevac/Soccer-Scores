using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scores.Database.Migrations
{
    public partial class StandingsSeasonStartEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SeasonEnd",
                table: "Standings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SeasonStart",
                table: "Standings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeasonEnd",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "SeasonStart",
                table: "Standings");
        }
    }
}
