using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archive_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class relationpersontree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_FamilyTree_FamilyTreeEntityId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_FamilyTreeEntityId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "FamilyTreeEntityId",
                table: "Person");

            migrationBuilder.AddColumn<Guid>(
                name: "ArchiveId",
                table: "Person",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ArchiveId",
                table: "Person",
                column: "ArchiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_FamilyTree_ArchiveId",
                table: "Person",
                column: "ArchiveId",
                principalTable: "FamilyTree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_FamilyTree_ArchiveId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_ArchiveId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ArchiveId",
                table: "Person");

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyTreeEntityId",
                table: "Person",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Person_FamilyTreeEntityId",
                table: "Person",
                column: "FamilyTreeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_FamilyTree_FamilyTreeEntityId",
                table: "Person",
                column: "FamilyTreeEntityId",
                principalTable: "FamilyTree",
                principalColumn: "Id");
        }
    }
}
