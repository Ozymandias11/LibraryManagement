using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class FinalEmailTemplateSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: new Guid("41c1f9d7-4c14-456b-8423-5114e1128995"));

           
            migrationBuilder.AlterColumn<string>(
                name: "TemplateName",
                table: "EmailTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

         

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "From", "Subject", "TemplateName", "To" },
                values: new object[] { new Guid("d71ed3c2-e74e-4a76-9a9e-288d12feadfd"), "Dear @@userName@@,<br><br>Please click the following link to verify your email address:<br><br>@@verificationLink@@<br><br>If you didn't request this verification, please ignore this email.<br><br>Best regards,<br>The Team", "noreply@example.com", "Verify Your Email Address", "Email Confirmation", "@@userEmail@@" });

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
