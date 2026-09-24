using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archive_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class updaterelationschapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Person_PersonEntityId",
                table: "Chapter");

            migrationBuilder.DropIndex(
                name: "IX_Chapter_PersonEntityId",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "PersonEntityId",
                table: "Chapter");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Chapter",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_PersonId",
                table: "Chapter",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Person_PersonId",
                table: "Chapter",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Person_PersonId",
                table: "Chapter");

            migrationBuilder.DropIndex(
                name: "IX_Chapter_PersonId",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Chapter");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonEntityId",
                table: "Chapter",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_PersonEntityId",
                table: "Chapter",
                column: "PersonEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Person_PersonEntityId",
                table: "Chapter",
                column: "PersonEntityId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
