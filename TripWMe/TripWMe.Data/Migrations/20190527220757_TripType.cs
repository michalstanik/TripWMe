using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripWMe.Data.Migrations
{
    public partial class TripType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripTypeId",
                table: "Trip",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TripType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TripTypeId",
                table: "Trip",
                column: "TripTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_TripType_TripTypeId",
                table: "Trip",
                column: "TripTypeId",
                principalTable: "TripType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_TripType_TripTypeId",
                table: "Trip");

            migrationBuilder.DropTable(
                name: "TripType");

            migrationBuilder.DropIndex(
                name: "IX_Trip_TripTypeId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "TripTypeId",
                table: "Trip");
        }
    }
}
