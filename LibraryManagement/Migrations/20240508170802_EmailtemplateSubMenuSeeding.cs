using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class EmailtemplateSubMenuSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            

           

            migrationBuilder.UpdateData(
                table: "NavigationMenus",
                keyColumn: "Id",
                keyValue: new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59"),
                column: "ActionName",
                value: "Templates");

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("07387a6b-45e3-45d4-a484-2af622a9d1e3"), new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("109a029a-d6ae-4171-a25a-5f5ff7761833"), new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("b86bf0ed-3f4b-4b76-a7f4-5ad87f29dd02"), new Guid("81a3994d-37ee-4833-a978-de19e7364514"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("bfadad78-5800-47db-bca7-42f451c0873a"), new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

         

            migrationBuilder.UpdateData(
                table: "NavigationMenus",
                keyColumn: "Id",
                keyValue: new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59"),
                column: "ActionName",
                value: "Users");

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("1ea1bc80-a04d-4aa0-9173-83a4d0ca2f93"), new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("27f4d3ba-78da-4437-9b9f-2e4bbcfc41d9"), new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("2ace72a0-e63b-4880-9329-63d50659e1e7"), new Guid("81a3994d-37ee-4833-a978-de19e7364514"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("9dcdf2f4-0a33-41b6-8e8d-0ea82acb6630"), new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" }
                });
        }
    }
}
