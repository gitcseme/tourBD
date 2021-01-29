using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Migrations.Notification
{
    public partial class AddedImageUrl_ToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotifierImageUrl",
                table: "Notifications",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifierImageUrl",
                table: "Notifications");
        }
    }
}
