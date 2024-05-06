using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class revertingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<bool>(
                name: "IsLate",
                table: "Reservations",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN [ActualReturnDate] IS NULL THEN CAST(NULL AS BIT) ELSE CAST(CASE WHEN [ActualReturnDate] > [SupposedReturnDate] THEN 1 ELSE 0 END AS BIT) END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "CASE WHEN [ActualReturnDate] IS NULL THEN CAST(0 AS BIT) ELSE CAST(CASE WHEN [ActualReturnDate] > [SupposedReturnDate] THEN 1 ELSE 0 END AS BIT) END");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<bool>(
                name: "IsLate",
                table: "Reservations",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN [ActualReturnDate] IS NULL THEN CAST(0 AS BIT) ELSE CAST(CASE WHEN [ActualReturnDate] > [SupposedReturnDate] THEN 1 ELSE 0 END AS BIT) END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "CASE WHEN [ActualReturnDate] IS NULL THEN CAST(NULL AS BIT) ELSE CAST(CASE WHEN [ActualReturnDate] > [SupposedReturnDate] THEN 1 ELSE 0 END AS BIT) END");

          
        }
    }
}
