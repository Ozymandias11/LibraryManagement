﻿using Library.Model.Models;
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
            var superAdminRoleId = "7984b858-30e3-4e98-a37e-a960b1b0bbee";

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
                  },
                   new RoleMenuPermission
                   {
                       Id = Guid.NewGuid(),
                       RoleId = adminRoleId,
                       NavigationMenuId = new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59")
                   },
                   new RoleMenuPermission
                   {
                       Id = Guid.NewGuid(),
                       RoleId = superAdminRoleId,
                       NavigationMenuId = new Guid("919bd644-16d7-483a-a6f7-aea8aa198ffd")
                   },
                   new RoleMenuPermission
                   {
                       Id = Guid.NewGuid(),
                       RoleId = superAdminRoleId,
                       NavigationMenuId = new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c")
                   },
                    new RoleMenuPermission
                    {
                        Id = Guid.NewGuid(),
                        RoleId = adminRoleId,
                        NavigationMenuId = new Guid("c555643a-c44b-4fb8-8fd0-cf5066b9efb9")
                    },
                    new RoleMenuPermission
                    {
                        Id = Guid.NewGuid(),
                        RoleId = adminRoleId,
                        NavigationMenuId = new Guid("8eb1fb90-24bc-4644-9139-74b6f425a83c")
                    },
                      new RoleMenuPermission
                      {
                          Id = Guid.NewGuid(),
                          RoleId = adminRoleId,
                          NavigationMenuId = new Guid("d35d1f1e-95ee-494c-96da-448d8677426e")
                      }

                );



        }
    }
}
