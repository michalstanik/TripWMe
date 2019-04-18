using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class AddTripCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TripCode",
                table: "Trip",
                nullable: true,
                computedColumnSql: "'TR-' + CONVERT(varchar(10),[Id])");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripCode",
                table: "Trip");
        }
    }
}
