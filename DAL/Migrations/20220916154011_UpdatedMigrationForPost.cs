using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facehook.DAL.Migrations
{
    public partial class UpdatedMigrationForPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "isSaved",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Posts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSaved",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
