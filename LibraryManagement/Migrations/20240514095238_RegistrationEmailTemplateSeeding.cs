using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationEmailTemplateSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: new Guid("d71ed3c2-e74e-4a76-9a9e-288d12feadfd"));

          


            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "From", "Subject", "TemplateName", "To" },
                values: new object[,]
                {
                    { new Guid("bcfb07af-932d-439f-bafe-db187f015f29"), "Dear @@userName@@,<br><br>Thank you for registering with our application. To complete your registration, please click the following link to verify your email address:<br><br><a href='@@verificationUrl@@'>Verify Email</a><br><br>If you did not request this verification, please ignore this email.<br><br>Best regards,<br>The Team", "noreply@example.com", "Verify Your Email Address", "Email Verification", "@@userEmail@@" },
                    { new Guid("d33e462e-0519-43c9-9f21-8c66dddf137d"), "Dear @@userName@@,<br><br>Please click the following link to verify your email address:<br><br>@@verificationLink@@<br><br>If you didn't request this verification, please ignore this email.<br><br>Best regards,<br>The Team", "noreply@example.com", "Verify Your Email Address", "Email Confirmation", "@@userEmail@@" }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
