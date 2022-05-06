﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DummyGram.Infrastructure.EFCore.Migrations
{
    public partial class AddedStoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfExpiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_IdUser",
                table: "Stories",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stories");
        }
    }
}
