using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Data.Migrations
{
    public partial class Changed_TourPackage_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "TourPackages");

            migrationBuilder.DropColumn(
                name: "PackageMemberSize",
                table: "TourPackages");

            migrationBuilder.DropColumn(
                name: "PackageNumber",
                table: "TourPackages");

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "TourPackages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "TourPackages",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PackageCode",
                table: "TourPackages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "TourPackages");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "TourPackages");

            migrationBuilder.DropColumn(
                name: "PackageCode",
                table: "TourPackages");

            migrationBuilder.AddColumn<double>(
                name: "DiscountedPrice",
                table: "TourPackages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PackageMemberSize",
                table: "TourPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageNumber",
                table: "TourPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
