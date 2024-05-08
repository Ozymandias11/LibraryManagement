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
    public class RoleMenuPermissionConfiguration : IEntityTypeConfiguration<RoleMenuPermission>
    {
        public void Configure(EntityTypeBuilder<RoleMenuPermission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.NavigationMenu)
                .WithMany()
                .HasForeignKey(x => x.NavigationMenuId);

            builder.HasOne<IdentityRole>()
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            var adminRoleId = "2a2e8e85-49f0-45ef-97c5-3151d1b91306";

            builder.HasData(
                new RoleMenuPermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = adminRoleId,
                    NavigationMenuId = new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876")
                },
                  new RoleMenuPermission
                  {
                      Id = Guid.NewGuid(),
                      RoleId = adminRoleId,
                      NavigationMenuId = new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637")
                  },
                  
                  new RoleMenuPermission
                  {
                      Id = Guid.NewGuid(),
                      RoleId = adminRoleId,
                      NavigationMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514")
                  }

                );



        }
    }
}
