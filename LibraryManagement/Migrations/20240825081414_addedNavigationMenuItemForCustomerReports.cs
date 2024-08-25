using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedNavigationMenuItemForCustomerReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[] { new Guid("aa7485cf-1bcb-4064-983e-ef4a730d9d1f"), "MonthlyRegistrationReport", "Customer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Report", new Guid("f513d8d7-b945-40d2-b679-2c2bbdd24f25"), true, null });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "NavigationMenuId", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("69ba3ed8-be27-4346-afa6-851487f1585d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("aa7485cf-1bcb-4064-983e-ef4a730d9d1f"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
