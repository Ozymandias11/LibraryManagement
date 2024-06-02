using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(x =>  x.Id);

            builder.Property(x => x.TemplateName).IsRequired();
            builder.Property(x => x.Subject).IsRequired();
            builder.Property(x => x.From).IsRequired();
            builder.Property(x => x.Body).IsRequired();


            builder.HasData(
                
                new EmailTemplate
                {
                    Id = Guid.NewGuid(),
                    TemplateName = "Email Confirmation",
                    From = "noreply@example.com",
                    To = "@@userEmail@@",
                    Subject = "Verify Your Email Address",
                    Body = "Dear @@userName@@,<br><br>Please click the following link to verify your email address:" +
                    "<br><br>@@verificationLink@@<br><br>If you didn't request this verification," +
                    " please ignore this email.<br><br>Best regards,<br>The Team"
                },
                new EmailTemplate
                {
                    Id = Guid.NewGuid(),
                    TemplateName = "Email Verification",
                    From = "noreply@example.com",
                    To = "@@userEmail@@",
                    Subject = "Verify Your Email Address",
                    Body = "Dear @@userName@@,<br><br>Thank you for registering with our application. " +
                           "To complete your registration, please click the following link to verify your email address:" +
                           "<br><br><a href='@@verificationUrl@@'>Verify Email</a><br><br>" +
                           "If you did not request this verification, please ignore this email." +
                           "<br><br>Best regards,<br>The Team"
                },
                new EmailTemplate
                {
                    Id = Guid.NewGuid(),
                    TemplateName = "Change Email Request",
                    From = "natchkebiadima1@gmail.com",
                    To = "@@userEmail@@",
                    Subject = "Email Change Request",
                    Body = "Dear @@userName@@,<br><br>We have received a request to change the email address associated with your account." +
                           " If you made this request, please click the following link to verify your new email address:" +
                           "<br><br><a href='@@resetLinkl@@'>Verify New Email</a><br><br>" +
                           "If you did not request this change, please ignore this email and your email address will remain the same." +
                           "<br><br>Best regards,<br>The Team"
                },
                new EmailTemplate
                {
                    Id = Guid.NewGuid(),
                    TemplateName = "Email Change Notification",
                    From = "natchkebiadima1@gmail.com",
                    To = "@@originalEmail@@",
                    Subject = "Email Change Request Notification",
                    Body = "Dear @@userName@@,<br><br>We have received a request to change the email address associated with your account" +
                       " If you made this request, no further action is required. Please verify your new email address using the link sent to it." +
                       "<br><br>If you did not request this change, please contact us immediately to secure your account." +
                       "<br><br>Best regards,<br>The Team"
                }

                );
        }
    }
}
