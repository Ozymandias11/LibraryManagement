using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedDynamicPrifileDropDownMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "Name", "ParentMenuId", "Permitted" },
                values: new object[] { new Guid("17a08e70-686e-4049-9746-b565a0fe8924"), null, null, "UserName", null, true });

          
            migrationBuilder.InsertData(
                table: "NavigationMenus",
                columns: new[] { "Id", "ActionName", "ControllerName", "Name", "ParentMenuId", "Permitted" },
                values: new object[] { new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7"), "Profile", "Account", "Profile", new Guid("17a08e70-686e-4049-9746-b565a0fe8924"), true });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "NavigationMenuId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("18db56da-fc65-4d96-9834-e309dc66c1db"), new Guid("17a08e70-686e-4049-9746-b565a0fe8924"), "b8b819da-4f16-4d09-9d10-7da416f2fb4b" },
                    { new Guid("95920b7c-8979-4f2e-883d-30aa429f8482"), new Guid("17a08e70-686e-4049-9746-b565a0fe8924"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45" },
                    { new Guid("95ec5f2f-e27b-4150-b3ec-8d8c10d899ad"), new Guid("17a08e70-686e-4049-9746-b565a0fe8924"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("f48e7c38-6406-46d4-908e-28a95c0a9a1f"), new Guid("17a08e70-686e-4049-9746-b565a0fe8924"), "7984b858-30e3-4e98-a37e-a960b1b0bbee" },
                    { new Guid("4c8760b1-12fc-48d3-aeae-50ed7c6c505f"), new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7"), "2a2e8e85-49f0-45ef-97c5-3151d1b91306" },
                    { new Guid("614fe969-ac54-499b-be71-76f9c9e73efa"), new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7"), "b8b819da-4f16-4d09-9d10-7da416f2fb4b" },
                    { new Guid("6273c142-63f9-4700-b6bb-97e8f4377901"), new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7"), "7984b858-30e3-4e98-a37e-a960b1b0bbee" },
                    { new Guid("f6b325ac-209e-4ade-ba25-64eec4e2f6e9"), new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7"), "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
