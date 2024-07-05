using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedReservationMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

          
            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[] { new Guid("f513d8d7-b945-40d2-b679-2c2bbdd24f25"), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Reservations", null, true, null });


            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[] { new Guid("10dc9edc-913d-4dda-a6ff-fe9065d575ee"), "Customers", "Customer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customers", new Guid("f513d8d7-b945-40d2-b679-2c2bbdd24f25"), true, null });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "NavigationMenuId", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f2d52c43-e72f-479b-bae7-79145ecdaf22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("f513d8d7-b945-40d2-b679-2c2bbdd24f25"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("8fe1cf81-8054-4a40-b7ab-e521c83d2404"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("10dc9edc-913d-4dda-a6ff-fe9065d575ee"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
