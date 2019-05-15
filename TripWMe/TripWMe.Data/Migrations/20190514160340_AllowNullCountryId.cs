using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class AllowNullCountryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Location",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
