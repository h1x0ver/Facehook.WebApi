using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facehook.DAL.Migrations
{
    public partial class CreatedFriendSuggestionStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserFriends",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserFriends");
        }
    }
}
