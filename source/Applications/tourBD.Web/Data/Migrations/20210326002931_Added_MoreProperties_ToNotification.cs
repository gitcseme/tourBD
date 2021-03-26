using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Data.Migrations
{
    public partial class Added_MoreProperties_ToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.AddColumn<Guid>(
                name: "NotifierId",
                table: "Notifications",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NotifierImageUrl",
                table: "Notifications",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NotifierName",
                table: "Notifications",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverId",
                table: "Notifications",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Notifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifierId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotifierImageUrl",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotifierName",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Notifications");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
