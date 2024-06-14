using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("2eb40a9c-2565-4c2c-a832-feca78f4603b"), "Publishers", "Publisher", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Publishers", null, true, null },
                    { new Guid("936a8edd-a127-4344-af9a-93ed9a32537f"), "Categories", "Category", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories", null, true, null },
                    { new Guid("9b96f51e-3d27-4eeb-b491-b6a75d8f0a01"), null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Books", null, true, null },
                    { new Guid("d3208527-a5f5-4e33-88c8-1a30af0e97e1"), "Authors", "Author", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Authors", null, true, null }
                });

       

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "CreatedDate", "DeletedDate", "Name", "ParentMenuId", "Permitted", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("092ac460-2e13-4e44-b207-7d44e3f5ca8b"), "BookCopies", "BookCopy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Book Copies", new Guid("9b96f51e-3d27-4eeb-b491-b6a75d8f0a01"), true, null },
                    { new Guid("d0c80121-b900-4515-8521-2737468ffa6a"), "Books", "Book", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Original Books", new Guid("9b96f51e-3d27-4eeb-b491-b6a75d8f0a01"), true, null }
                });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "NavigationMenuId", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("654ff627-b020-4e36-8605-acb1e496dea2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("936a8edd-a127-4344-af9a-93ed9a32537f"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("8704f251-96f4-4cf5-bf37-b31607c254cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("2eb40a9c-2565-4c2c-a832-feca78f4603b"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("a7ae0281-e0a1-4d76-8791-2c4c352b1a3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("d3208527-a5f5-4e33-88c8-1a30af0e97e1"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("d5e345b6-1036-4974-97a6-fb18c4dd935f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("936a8edd-a127-4344-af9a-93ed9a32537f"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("222fd289-df68-400d-8fc2-69eab3cfc0d5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("092ac460-2e13-4e44-b207-7d44e3f5ca8b"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null },
                    { new Guid("cf04362a-d65b-43a7-a6a9-ade5303942e8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("d0c80121-b900-4515-8521-2737468ffa6a"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
