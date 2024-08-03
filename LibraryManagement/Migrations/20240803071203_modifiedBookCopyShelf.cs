using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class modifiedBookCopyShelf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stored_BookCopies_BookCopyId",
                table: "Stored");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stored",
                table: "Stored");

            migrationBuilder.RenameColumn(
                name: "BookCopyId",
                table: "Stored",
                newName: "BookCopyShelfId");

            migrationBuilder.AddColumn<Guid>(
                name: "BookCopyShelfId",
                table: "BookCopies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stored",
                table: "Stored",
                column: "BookCopyShelfId");

          

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_BookCopyShelfId",
                table: "BookCopies",
                column: "BookCopyShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopies_Stored_BookCopyShelfId",
                table: "BookCopies",
                column: "BookCopyShelfId",
                principalTable: "Stored",
                principalColumn: "BookCopyShelfId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
