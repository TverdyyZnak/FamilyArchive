using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archive_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class strupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_FamilyTree_FamilyTreeEntityId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_FamilyTreeEntityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FamilyTreeEntityId",
                table: "User");

            migrationBuilder.CreateTable(
                name: "FamilyTreeUsers",
                columns: table => new
                {
                    FamilyTreesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyTreeUsers", x => new { x.FamilyTreesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_FamilyTreeUsers_FamilyTree_FamilyTreesId",
                        column: x => x.FamilyTreesId,
                        principalTable: "FamilyTree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyTreeUsers_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyTree_MainUserId",
                table: "FamilyTree",
                column: "MainUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyTreeUsers_UsersId",
                table: "FamilyTreeUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyTree_User_MainUserId",
                table: "FamilyTree",
                column: "MainUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyTree_User_MainUserId",
                table: "FamilyTree");

            migrationBuilder.DropTable(
                name: "FamilyTreeUsers");

            migrationBuilder.DropIndex(
                name: "IX_FamilyTree_MainUserId",
                table: "FamilyTree");

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyTreeEntityId",
                table: "User",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_User_FamilyTreeEntityId",
                table: "User",
                column: "FamilyTreeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_FamilyTree_FamilyTreeEntityId",
                table: "User",
                column: "FamilyTreeEntityId",
                principalTable: "FamilyTree",
                principalColumn: "Id");
        }
    }
}
