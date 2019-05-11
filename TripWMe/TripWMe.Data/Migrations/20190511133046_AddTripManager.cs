using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class AddTripManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TripManagerId",
                table: "Trip",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TripManagerId",
                table: "Trip",
                column: "TripManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_AspNetUsers_TripManagerId",
                table: "Trip",
                column: "TripManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_AspNetUsers_TripManagerId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_TripManagerId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "TripManagerId",
                table: "Trip");
        }
    }
}
