using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class SecondSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumn: "Id",
                keyValue: new Guid("ac83b1b4-8c0f-4ea1-b2ba-120cd2ecf1ba"));

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumn: "Id",
                keyValue: new Guid("bff5fe47-2765-475c-84c8-b1614e250d86"));

          

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("639c7cb4-7ca3-4832-9b23-6ef5db6b4731"), new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("67a94cf1-543c-4aa3-9f59-41d121d05205"), new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("a2ad2549-e503-4b08-96f6-824d4b5d68ab"), new Guid("81a3994d-37ee-4833-a978-de19e7364514"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumn: "Id",
                keyValue: new Guid("639c7cb4-7ca3-4832-9b23-6ef5db6b4731"));

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumn: "Id",
                keyValue: new Guid("67a94cf1-543c-4aa3-9f59-41d121d05205"));

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumn: "Id",
                keyValue: new Guid("a2ad2549-e503-4b08-96f6-824d4b5d68ab"));

          

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("ac83b1b4-8c0f-4ea1-b2ba-120cd2ecf1ba"), new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("bff5fe47-2765-475c-84c8-b1614e250d86"), new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" }
                });
        }
    }
}
