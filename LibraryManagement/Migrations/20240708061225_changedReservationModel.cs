using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class changedReservationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "IsLate",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BookCopies_BookCopyID",
                table: "Reservations");


            migrationBuilder.RenameColumn(
                name: "BookCopyID",
                table: "Reservations",
                newName: "BookCopyId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_BookCopyID",
                table: "Reservations",
                newName: "IX_Reservations_BookCopyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReturnCustomerId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookCopyId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualReturnDate",
                table: "Reservations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "ReservationItem",
                columns: table => new
                {
                    ReservationItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookCopyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActualReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationItem", x => x.ReservationItemId);
                    table.ForeignKey(
                        name: "FK_ReservationItem_BookCopies_BookCopyID",
                        column: x => x.BookCopyID,
                        principalTable: "BookCopies",
                        principalColumn: "BookCopyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationItem_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<bool>(
            name: "IsLate",
            table: "Reservations",
            nullable: false,
            computedColumnSql: "CASE WHEN [ActualReturnDate] IS NULL THEN CAST(NULL AS BIT) ELSE CAST(CASE WHEN [ActualReturnDate] > [SupposedReturnDate] THEN 1 ELSE 0 END AS BIT) END");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_BookCopyID",
                table: "ReservationItem",
                column: "BookCopyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_ReservationId",
                table: "ReservationItem",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BookCopies_BookCopyId",
                table: "Reservations",
                column: "BookCopyId",
                principalTable: "BookCopies",
                principalColumn: "BookCopyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
