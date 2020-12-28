using Microsoft.EntityFrameworkCore.Migrations;

namespace Scores.Database.Migrations
{
    public partial class ClubLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Clubs",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Clubs");
        }
    }
}
