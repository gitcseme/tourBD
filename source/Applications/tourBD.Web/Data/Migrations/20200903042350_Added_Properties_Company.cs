using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Data.Migrations
{
    public partial class Added_Properties_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reputation",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Star",
                table: "Companies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Star",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "Reputation",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
