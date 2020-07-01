using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Data.Migrations
{
    public partial class IsVarifiedpropertyadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVarified",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVarified",
                table: "AspNetUsers");
        }
    }
}
