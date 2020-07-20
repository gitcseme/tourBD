using Microsoft.EntityFrameworkCore.Migrations;

namespace tourBD.Web.Data.Migrations
{
    public partial class AddedAuthorImageUrlProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorImageUrl",
                table: "Replays",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorImageUrl",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorImageUrl",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorImageUrl",
                table: "Replays");

            migrationBuilder.DropColumn(
                name: "AuthorImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorImageUrl",
                table: "Comments");
        }
    }
}
