using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRoomEmployeeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ce36ef2-b47d-4fcb-8460-5d2904ca34eb", null, "Librarian", "LIBRARIAN" },
                    { "5e30375a-b21e-479c-968f-805a025e3656", null, "Manager", "MANAGER" },
                    { "6117cbd7-d45c-49ca-bf58-7a984ed1741d", null, "Archivist", "ARCHIVIST" },
                    { "94f26ced-4ff4-4767-bdc9-f03e771265cd", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ce36ef2-b47d-4fcb-8460-5d2904ca34eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e30375a-b21e-479c-968f-805a025e3656");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6117cbd7-d45c-49ca-bf58-7a984ed1741d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94f26ced-4ff4-4767-bdc9-f03e771265cd");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomId",
                table: "AspNetUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
