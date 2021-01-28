using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Data.Migrations
{
    public partial class Adding_Loves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Love_TourPackages_TourPackageId",
                table: "Love");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Love",
                table: "Love");

            migrationBuilder.RenameTable(
                name: "Love",
                newName: "Loves");

            migrationBuilder.RenameIndex(
                name: "IX_Love_TourPackageId",
                table: "Loves",
                newName: "IX_Loves_TourPackageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loves",
                table: "Loves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loves_TourPackages_TourPackageId",
                table: "Loves",
                column: "TourPackageId",
                principalTable: "TourPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loves_TourPackages_TourPackageId",
                table: "Loves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loves",
                table: "Loves");

            migrationBuilder.RenameTable(
                name: "Loves",
                newName: "Love");

            migrationBuilder.RenameIndex(
                name: "IX_Loves_TourPackageId",
                table: "Love",
                newName: "IX_Love_TourPackageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Love",
                table: "Love",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Love_TourPackages_TourPackageId",
                table: "Love",
                column: "TourPackageId",
                principalTable: "TourPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
