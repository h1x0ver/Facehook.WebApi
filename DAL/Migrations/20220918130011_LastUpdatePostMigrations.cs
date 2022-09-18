using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facehook.DAL.Migrations
{
    public partial class LastUpdatePostMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DislikeCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DislikeCount",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Posts",
                type: "int",
                nullable: true);
        }
    }
}
