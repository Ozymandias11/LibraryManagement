using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedSuperAdminMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "Name", "ParentMenuId", "Permitted" },
                values: new object[] { new Guid("919bd644-16d7-483a-a6f7-aea8aa198ffd"), null, null, "Super Admin", null, true });


            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "Name", "ParentMenuId", "Permitted" },
                values: new object[] { new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c"), "Users", "SuperAdminController", "Users", new Guid("919bd644-16d7-483a-a6f7-aea8aa198ffd"), true });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0f06e37e-aacf-4d61-9510-c8b86fa8bb81"), new Guid("919bd644-16d7-483a-a6f7-aea8aa198ffd"), "7984b858-30e3-4e98-a37e-a960b1b0bbee" },
                    { new Guid("1f0ac30a-9af6-4851-9aee-5e46e95ae5fb"), new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c"), "7984b858-30e3-4e98-a37e-a960b1b0bbee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
