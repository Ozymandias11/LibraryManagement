using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.FirstName).IsRequired();

            builder.Property(c => c.LastName).IsRequired();

            builder.Property(c => c.Address).IsRequired();


            builder.Property(c => c.Email)
                .IsRequired()
                .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .HasAnnotation("RegularExpressionErrorMessage", "Invalid email address format");

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasAnnotation("RegularExpression", @"^\+\d{1,3}\s?\(?\d{1,4}\)?[\s.-]?\d{1,10}$")
                .HasAnnotation("RegularExpressionErrorMessage", "Invalid phone number format");


            builder.HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerID);



        }
    }
}
