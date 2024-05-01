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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);


            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();


           


            builder.HasMany(e => e.Reservations)
            .WithOne(r => r.Employee)
            .HasForeignKey(r => r.EmployeeId)
            .IsRequired();

           


        }
    }
}
