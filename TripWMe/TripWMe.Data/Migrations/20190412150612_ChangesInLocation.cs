using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class ChangesInLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "Location",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LocationDescription",
                table: "Location",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Alpha2Code",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alpha2Code",
                table: "Country");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Location",
                newName: "LocationName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Location",
                newName: "LocationDescription");
        }
    }
}
