using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedingForEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: new Guid("f384815b-12eb-4ecf-94b9-e8a989dd5df8"));

            

            migrationBuilder.AddColumn<string>(
                name: "TemplateName",
                table: "EmailTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "From", "Subject", "TemplateName", "To" },
                values: new object[] { new Guid("08079ca3-2271-4407-a4ee-be408a5e0c39"), "Dear @@userName@@,<br><br>Please click the following link to verify your email address:<br><br>@@verificationLink@@<br><br>If you didn't request this verification, please ignore this email.<br><br>Best regards,<br>The Team", "noreply@example.com", "Verify Your Email Address", "Email Confirmation", "@@userEmail@@" });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: new Guid("08079ca3-2271-4407-a4ee-be408a5e0c39"));

          

            migrationBuilder.DropColumn(
                name: "TemplateName",
                table: "EmailTemplate");

          

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "From", "Subject", "To" },
                values: new object[] { new Guid("f384815b-12eb-4ecf-94b9-e8a989dd5df8"), "Dear @@userName@@,<br><br>Please click the following link to verify your email address:<br><br>@@verificationLink@@<br><br>If you didn't request this verification, please ignore this email.<br><br>Best regards,<br>The Team", "noreply@example.com", "Verify Your Email Address", "@@userEmail@@" });

          
        }
    }
}
