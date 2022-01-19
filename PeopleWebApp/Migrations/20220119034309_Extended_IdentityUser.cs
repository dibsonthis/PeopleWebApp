using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleWebApp.Migrations
{
    public partial class Extended_IdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "People",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_AppUserId",
                table: "People",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AppUserId",
                table: "People",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AppUserId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_AppUserId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "People");
        }
    }
}
