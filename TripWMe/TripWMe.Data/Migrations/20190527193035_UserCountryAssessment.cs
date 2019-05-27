using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class UserCountryAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCountryAssessment",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false),
                    TUserId = table.Column<string>(nullable: false),
                    AreaLevelAssessment = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCountryAssessment", x => new { x.CountryId, x.TUserId });
                    table.ForeignKey(
                        name: "FK_UserCountryAssessment_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCountryAssessment_AspNetUsers_TUserId",
                        column: x => x.TUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCountryAssessment_TUserId",
                table: "UserCountryAssessment",
                column: "TUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCountryAssessment");
        }
    }
}
