using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archive_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class tree_log_rebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usersId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "usersId",
                table: "FamilyTree",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
