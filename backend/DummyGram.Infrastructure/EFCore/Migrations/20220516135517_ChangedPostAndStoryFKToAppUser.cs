using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DummyGram.Infrastructure.EFCore.Migrations
{
    public partial class ChangedPostAndStoryFKToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_IdUser",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AspNetUsers_IdUser",
                table: "Stories");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AppUsers_IdUser",
                table: "Posts",
                column: "IdUser",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AppUsers_IdUser",
                table: "Stories",
                column: "IdUser",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AppUsers_IdUser",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AppUsers_IdUser",
                table: "Stories");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_IdUser",
                table: "Posts",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AspNetUsers_IdUser",
                table: "Stories",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
