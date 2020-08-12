using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Data.Migrations
{
    public partial class Added_MoreProperties_to_TourPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountedPrice",
                table: "TourPackages",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "TourPackages",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PackageMemberSize",
                table: "TourPackages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "TourPackages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TourPackages");

            migrationBuilder.DropColumn(
                name: "PackageMemberSize",
                table: "TourPackages");
        }
    }
}
