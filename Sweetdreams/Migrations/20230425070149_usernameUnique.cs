using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sweetdreams.Migrations
{
    public partial class usernameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName_Email",
                table: "AspNetUsers",
                columns: new[] { "UserName", "Email" },
                unique: true,
                filter: "[UserName] IS NOT NULL AND [Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName_Email",
                table: "AspNetUsers");
        }
    }
}
