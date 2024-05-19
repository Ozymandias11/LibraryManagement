using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7984b858-30e3-4e98-a37e-a960b1b0bbee", null, "SuperAdmin", "SUPERADMIN" },
                  
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreationDate", "DeleteDate", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoomId", "SecurityStamp", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[] { "3d56b21f-accc-450c-8b2a-98d1d386007e", 0, "71fd3e24-5ca3-4295-8ca7-1186f71c713b", new DateTime(2024, 5, 18, 6, 55, 22, 576, DateTimeKind.Utc).AddTicks(4136), null, "nachkebiadima2@gmail.com", true, "Super", "Admin", false, null, null, null, "AQAAAAIAAYagAAAAEBxOIgasshEIjqV1+Cewur4Eaj55C0C7V5/SrLRiVr91k1Dk9E7P5L5uLoHbFy0iAg==", null, false, null, "99021ed2-c985-470f-9efc-3f344de442ae", false, null, "nachkebiadima2@gmail.com" });

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
