using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DummyGram.Infrastructure.EFCore.Migrations
{
    public partial class AddedUserSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscribersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptionsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => new { x.SubscribersId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_Subscriptions_AppUsers_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_AppUsers_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionsId",
                table: "Subscriptions",
                column: "SubscriptionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
