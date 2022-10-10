using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facehook.DAL.Migrations
{
    public partial class MaybeLastMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_AppUserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "LikedDate",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Likes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Likes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Likes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Likes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LikedDate",
                table: "Likes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AppUserId",
                table: "Likes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId",
                table: "Likes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
