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
                      }, 
                      new NavigationMenu
                      {
                          Id = new Guid("919bd644-16d7-483a-a6f7-aea8aa198ffd"),
                          Name = "Super Admin",
                          ControllerName = null,
                          ActionName = null,
                          Permitted = true,
                      },
                      new NavigationMenu
                      {
                          Id = new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c"),
                          Name = "Users",
                          ParentMenuId = new Guid("919bd644-16d7-483a-a6f7-aea8aa198ffd"),
                          ControllerName = "SuperAdminController",
                          ActionName = "Users",
                          Permitted = true,
                      },
                       new NavigationMenu
                       {
                           Id = new Guid("c555643a-c44b-4fb8-8fd0-cf5066b9efb9"),
                           Name = "Pending Users",
                           ParentMenuId = new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c"),
                           ControllerName = "Administrator",
                           ActionName = "Roles",
                           Permitted = true,
                       },
                       new NavigationMenu
                       {
                           Id = new Guid("8eb1fb90-24bc-4644-9139-74b6f425a83c"),
                           Name = "Active Users",
                           ParentMenuId = new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c"),
                           ControllerName = "Administrator",
                           ActionName = "Users",
                           Permitted = true,
                       },
                        new NavigationMenu
                        {
                            Id = new Guid("d35d1f1e-95ee-494c-96da-448d8677426e"),
                            Name = "Deletede Users",
                            ParentMenuId = new Guid("62c3dd88-8cdd-47b6-b212-053623fe245c"),
                            ControllerName = "Administrator",
                            ActionName = "DeletedUsers",
                            Permitted = true,
                        },
                           new NavigationMenu
                           {
                               Id = new Guid("17a08e70-686e-4049-9746-b565a0fe8924"),
                               Name = "UserName",
                               ParentMenuId = null,
                               ControllerName = null,
                               ActionName = null,
                               Permitted = true,
                           },
                             new NavigationMenu
                             {
                                 Id = new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7"),
                                 Name = "Profile",
                                 ParentMenuId = new Guid("17a08e70-686e-4049-9746-b565a0fe8924"),
                                 ControllerName = "Account",
                                 ActionName = "Profile",
                                 Permitted = true,
                             }

                );





        }
    }
}
