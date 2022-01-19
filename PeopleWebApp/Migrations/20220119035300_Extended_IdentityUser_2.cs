using Microsoft.EntityFrameworkCore.Migrations;

namespace PeopleWebApp.Migrations
{
    public partial class Extended_IdentityUser_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "People");
        }
    }
}
