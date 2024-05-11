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
    public class NavigationMenuConfiguration : IEntityTypeConfiguration<NavigationMenu>
    {
        public void Configure(EntityTypeBuilder<NavigationMenu> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                 .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired();

            
            builder.HasOne(x => x.ParentNavigationMenu)
                .WithMany()
                .HasForeignKey(x => x.ParentMenuId);

            

            builder.HasData(
                   new NavigationMenu
                   {
                       Id = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                       Name = "Admin",
                       ControllerName = null,
                       ActionName = null,
                       Permitted = true,
                   },
                   new NavigationMenu
                   {
                       Id = new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"),
                       Name = "Roles",
                       ParentMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                       ControllerName = "Administrator",
                       ActionName = "Roles",
                       Permitted = true,
                   },
                     new NavigationMenu
                     {
                         Id = new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"),
                         Name = "Users",
                         ParentMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                         ControllerName = "Administrator",
                         ActionName = "Users",
                         Permitted = true,
                     },
                      new NavigationMenu
                      {
                          Id = new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59"),
                          Name = "Email Templates",
                          ParentMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                          ControllerName = "Administrator",
                          ActionName = "Templates",
                          Permitted = true,
                      }

                );





        }
    }
}
