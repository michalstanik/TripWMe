using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class ChangeInWorldHeritage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "WorldHeritage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "WorldHeritage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "WorldHeritage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "WorldHeritage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "WorldHeritage");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "WorldHeritage");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "WorldHeritage");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "WorldHeritage");
        }
    }
}
