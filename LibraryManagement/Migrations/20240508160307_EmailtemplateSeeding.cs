using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class EmailtemplateSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

          
            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "From", "Subject", "To" },
                values: new object[] { new Guid("cd41ba9a-6f9d-4676-90e2-fd33be2e2376"), "Dear @@userName@@,<br><br>Please click the following link to verify your email address:<br><br>@@verificationLink@@<br><br>If you didn't request this verification, please ignore this email.<br><br>Best regards,<br>The Team", "noreply@example.com", "Verify Your Email Address", "@@userEmail@@" });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplate");

          

           
        }
    }
}
