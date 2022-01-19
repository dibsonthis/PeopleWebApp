using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleWebApp.Migrations
{
    public partial class Extended_IdentityUser_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AppUserId",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "People",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_People_AppUserId",
                table: "People",
                newName: "IX_People_AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AppUserID",
                table: "People",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_AspNetUsers_AppUserID",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "People",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_People_AppUserID",
                table: "People",
                newName: "IX_People_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_AspNetUsers_AppUserId",
                table: "People",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
