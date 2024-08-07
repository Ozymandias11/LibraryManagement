﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                  new IdentityRole
                  {
                      Name = "Librarian",
                      NormalizedName = "LIBRARIAN"
                  }, 
                  new IdentityRole
                  {
                      Name = "Administrator", 
                      NormalizedName = "ADMINISTRATOR"
                  }, 
                  new IdentityRole
                  {
                      Name = "Manager", 
                      NormalizedName = "MANAGER"
                  }, 
                  new IdentityRole
                  {
                      Name = "SuperAdmin", 
                      NormalizedName = "SUPERADMIN"
                  },
                   new IdentityRole
                   {
                       Name = "Default",
                       NormalizedName = "DEFAULT"
                   }

                );
        }
    }
}
