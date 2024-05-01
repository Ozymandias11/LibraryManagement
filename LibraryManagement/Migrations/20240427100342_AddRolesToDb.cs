using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ea2bb40-e9f3-4c3d-95d8-e5d4dc8aaeee", null, "Manager", "MANAGER" },
                    { "b53be266-4fc6-45b3-adeb-b09afff739d8", null, "Administrator", "ADMINISTRATOR" },
                    { "ec6ba672-7159-451b-9a87-00d365b439f6", null, "Librarian", "LIBRARIAN" },
                    { "f63b839a-8d78-43aa-8d66-d3db593d5b94", null, "Archivist", "ARCHIVIST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ea2bb40-e9f3-4c3d-95d8-e5d4dc8aaeee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b53be266-4fc6-45b3-adeb-b09afff739d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec6ba672-7159-451b-9a87-00d365b439f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f63b839a-8d78-43aa-8d66-d3db593d5b94");
        }
    }
}
