using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class RemoveTripStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripStats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripStats",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false),
                    CountryCount = table.Column<int>(nullable: false),
                    LocationCount = table.Column<int>(nullable: false),
                    UserCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripStats", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_TripStats_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
