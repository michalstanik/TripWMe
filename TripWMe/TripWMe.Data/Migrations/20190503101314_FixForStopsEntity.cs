using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class FixForStopsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stop_Trip_TripId",
                table: "Stop");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "Stop",
                newName: "tripId");

            migrationBuilder.RenameIndex(
                name: "IX_Stop_TripId",
                table: "Stop",
                newName: "IX_Stop_tripId");

            migrationBuilder.AlterColumn<int>(
                name: "tripId",
                table: "Stop",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stop_Trip_tripId",
                table: "Stop",
                column: "tripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stop_Trip_tripId",
                table: "Stop");

            migrationBuilder.RenameColumn(
                name: "tripId",
                table: "Stop",
                newName: "TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Stop_tripId",
                table: "Stop",
                newName: "IX_Stop_TripId");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Stop",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Stop_Trip_TripId",
                table: "Stop",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
