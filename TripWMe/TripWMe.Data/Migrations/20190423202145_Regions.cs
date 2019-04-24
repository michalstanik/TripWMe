using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class Regions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_Continent_ContinentId",
                table: "Country");

            migrationBuilder.RenameColumn(
                name: "ContinentId",
                table: "Country",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Country_ContinentId",
                table: "Country",
                newName: "IX_Country_RegionId");

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    ContinentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Region_ContinentId",
                table: "Region",
                column: "ContinentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Region_RegionId",
                table: "Country",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_Region_RegionId",
                table: "Country");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Country",
                newName: "ContinentId");

            migrationBuilder.RenameIndex(
                name: "IX_Country_RegionId",
                table: "Country",
                newName: "IX_Country_ContinentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Continent_ContinentId",
                table: "Country",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
