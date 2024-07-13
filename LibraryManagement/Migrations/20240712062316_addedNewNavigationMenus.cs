using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedNewNavigationMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            


            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[] { new Guid("9518e097-f106-497b-aac5-a3c5e7100aed"), "Reservations", "Reservation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Current Reservations", new Guid("f513d8d7-b945-40d2-b679-2c2bbdd24f25"), true, null });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "NavigationMenuId", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("430501cd-4e8a-48a8-a4d1-e2877a53cc06"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("9518e097-f106-497b-aac5-a3c5e7100aed"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
