using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class Addedmoreemailtemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

      

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "Body", "From", "Subject", "TemplateName", "To" },
                values: new object[,]
                {
                    { new Guid("332a502b-5e21-4792-8705-fe1a3da8c698"), "Dear @@userName@@,<br><br>We have received a request to change the email address associated with your account. If you made this request, please click the following link to verify your new email address:<br><br><a href='@@resetLinkl@@'>Verify New Email</a><br><br>If you did not request this change, please ignore this email and your email address will remain the same.<br><br>Best regards,<br>The Team", "natchkebiadima1@gmail.com", "Email Change Request", "Change Email Request", "@@userEmail@@" },
                    { new Guid("42ab0dc1-f979-4558-8ade-b9cb14255e58"), "Dear @@userName@@,<br><br>We have received a request to change the email address associated with your account If you made this request, no further action is required. Please verify your new email address using the link sent to it.<br><br>If you did not request this change, please contact us immediately to secure your account.<br><br>Best regards,<br>The Team", "natchkebiadima1@gmail.com", "Email Change Request Notification", "Email Change Notification", "@@originalEmail@@" },
                   
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
