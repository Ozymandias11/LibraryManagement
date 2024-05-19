using Library.Model.Models;
using Microsoft.AspNetCore.Identity;
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
            builder.Property(e => e.DateOfBirth).IsRequired();





            builder.HasMany(e => e.Reservations)
            .WithOne(r => r.Employee)
            .HasForeignKey(r => r.EmployeeId)
            .IsRequired();


            var superAdminId = Guid.NewGuid().ToString();
            var superAdminUser = new Employee
            {
                Id = superAdminId,
                UserName = "nachkebiadima2@gmail.com",
                Email = "nachkebiadima2@gmail.com",
                EmailConfirmed = true,
                FirstName = "Super",
                LastName = "Admin",
                CreationDate = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var passwordHasher = new PasswordHasher<Employee>();
            superAdminUser.PasswordHash = passwordHasher.HashPassword(superAdminUser, "superadmin1234");

            builder.HasData(superAdminUser);






        }
    }
}
