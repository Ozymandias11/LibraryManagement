using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedNavigationMenuItemForRDLCReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[] { new Guid("f40e9642-65fc-4fc2-a2b8-677c27b9f742"), "RDLCReport", "Report", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "RDLC Report", null, true, null });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "NavigationMenuId", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("d7c8bb76-df9c-4809-9868-fcd7ac1530e2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("f40e9642-65fc-4fc2-a2b8-677c27b9f742"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
