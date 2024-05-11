using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: new Guid("013f3261-0a3b-4753-a98d-463668414616"));

         

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "From", "Subject", "TemplateName", "To" },
                values: new object[] { new Guid("41c1f9d7-4c14-456b-8423-5114e1128995"), "Dear @@userName@@,<br><br>Please click the following link to verify your email address:<br><br>@@verificationLink@@<br><br>If you didn't request this verification, please ignore this email.<br><br>Best regards,<br>The Team", "noreply@example.com", "Verify Your Email Address", "Email Confirmation", "@@userEmail@@" });

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
