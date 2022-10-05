using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facehook.DAL.Migrations
{
    public partial class CreatedMigrationsForStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AspNetUsers_UserId1",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_UserId1",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "StoryFileName",
                table: "Stories",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Stories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_UserId",
                table: "Stories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_StoryId",
                table: "Images",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Stories_StoryId",
                table: "Images",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AspNetUsers_UserId",
                table: "Stories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Stories_StoryId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AspNetUsers_UserId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_UserId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Images_StoryId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Stories",
                newName: "StoryFileName");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Stories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_UserId1",
                table: "Stories",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AspNetUsers_UserId1",
                table: "Stories",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
