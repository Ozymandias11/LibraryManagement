using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubSubMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "Name", "ParentMenuId", "Permitted" },
                values: new object[] { new Guid("8eb1fb90-24bc-4644-9139-74b6f425a83c"), "Users", "Administrator", "Active Users", new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c"), true });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("5b359fe5-bce0-4097-9c5b-c85503c6a33f"), new Guid("8eb1fb90-24bc-4644-9139-74b6f425a83c"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
