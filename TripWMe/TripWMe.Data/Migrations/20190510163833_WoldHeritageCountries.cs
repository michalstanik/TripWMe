using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class WoldHeritageCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorldHeritageCountry",
                columns: table => new
                {
                    WorldHeritageId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorldHeritageCountry", x => new { x.WorldHeritageId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_WorldHeritageCountry_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorldHeritageCountry_WorldHeritage_WorldHeritageId",
                        column: x => x.WorldHeritageId,
                        principalTable: "WorldHeritage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorldHeritageCountry_CountryId",
                table: "WorldHeritageCountry",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorldHeritageCountry");
        }
    }
}
