using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedNavigationMenuItemsForDropDown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[] { new Guid("3d88fdda-1aa4-40da-a58a-0fa78d5e05c9"), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Reports", new Guid("9b96f51e-3d27-4eeb-b491-b6a75d8f0a01"), true, null });

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("72bced4d-6e72-4201-bcfe-1690f2115a76"), "LostBooksReport", "Book", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lost Books", new Guid("3d88fdda-1aa4-40da-a58a-0fa78d5e05c9"), true, null },
                    { new Guid("db4fd8bd-e040-49c8-bea8-57583f1ab369"), "PopularityReport", "Book", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Popularity", new Guid("3d88fdda-1aa4-40da-a58a-0fa78d5e05c9"), true, null }
                });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "NavigationMenuId", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("4ce27fb2-d172-4874-b9b9-dd57a4550376"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("3d88fdda-1aa4-40da-a58a-0fa78d5e05c9"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("a1e1293a-7779-411e-8b45-3d123c5bfe54"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("72bced4d-6e72-4201-bcfe-1690f2115a76"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("e657f990-ad52-4bea-af38-b102a7a289df"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("db4fd8bd-e040-49c8-bea8-57583f1ab369"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
