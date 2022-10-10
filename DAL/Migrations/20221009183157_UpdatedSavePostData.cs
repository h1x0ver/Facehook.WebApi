using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facehook.DAL.Migrations
{
    public partial class UpdatedSavePostData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavePosts_AspNetUsers_AppUserId",
                table: "SavePosts");

            migrationBuilder.DropIndex(
                name: "IX_SavePosts_AppUserId",
                table: "SavePosts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "SavePosts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SavePosts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_UserId",
                table: "SavePosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavePosts_AspNetUsers_UserId",
                table: "SavePosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavePosts_AspNetUsers_UserId",
                table: "SavePosts");

            migrationBuilder.DropIndex(
                name: "IX_SavePosts_UserId",
                table: "SavePosts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SavePosts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "SavePosts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavePosts_AppUserId",
                table: "SavePosts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavePosts_AspNetUsers_AppUserId",
                table: "SavePosts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
