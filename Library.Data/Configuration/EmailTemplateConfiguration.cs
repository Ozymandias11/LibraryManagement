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
                }

                );
        }
    }
}
