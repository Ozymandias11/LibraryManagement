using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoleMenupermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "NavigationMenus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "NavigationMenus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

           

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "Name", "ParentMenuId", "Permitted" },
                values: new object[,]
                {
                    { new Guid("81a3994d-37ee-4833-a978-de19e7364514"), null, null, "Admin", null, true },
                    { new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"), "Users", "Administrator", "Users", new Guid("81a3994d-37ee-4833-a978-de19e7364514"), true },
                    { new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"), "Roles", "Administrator", "Roles", new Guid("81a3994d-37ee-4833-a978-de19e7364514"), true }
                });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("ac83b1b4-8c0f-4ea1-b2ba-120cd2ecf1ba"), new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("bff5fe47-2765-475c-84c8-b1614e250d86"), new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumn: "Id",
                keyValue: new Guid("ac83b1b4-8c0f-4ea1-b2ba-120cd2ecf1ba"));

            migrationBuilder.DeleteData(
                table: "RoleMenus",
                keyColumn: "Id",
                keyValue: new Guid("bff5fe47-2765-475c-84c8-b1614e250d86"));

            migrationBuilder.DeleteData(
                table: "NavigationMenus",
                keyColumn: "Id",
                keyValue: new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"));

            migrationBuilder.DeleteData(
                table: "NavigationMenus",
                keyColumn: "Id",
                keyValue: new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"));

            migrationBuilder.DeleteData(
                table: "NavigationMenus",
                keyColumn: "Id",
                keyValue: new Guid("81a3994d-37ee-4833-a978-de19e7364514"));

            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "NavigationMenus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "NavigationMenus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

          
        }
    }
}
