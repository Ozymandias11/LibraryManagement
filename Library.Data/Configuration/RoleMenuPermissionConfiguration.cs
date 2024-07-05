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
            var superAdminRoleId = "7984b858-30e3-4e98-a37e-a960b1b0bbee";
            var librarianRoleId = "4ca481bb-5e65-4a13-8a4c-c75e93e0ac45";
            var managerRoleId = "b8b819da-4f16-4d09-9d10-7da416f2fb4b";

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
                      //
                      ,
                       new RoleMenuPermission
                       {
                           Id = Guid.NewGuid(),
                           RoleId = adminRoleId,
                           NavigationMenuId = new Guid("17a08e70-686e-4049-9746-b565a0fe8924")
                       },
                        new RoleMenuPermission
                        {
                            Id = Guid.NewGuid(),
                            RoleId = librarianRoleId,
                            NavigationMenuId = new Guid("17a08e70-686e-4049-9746-b565a0fe8924")
                        },
                         new RoleMenuPermission
                         {
                             Id = Guid.NewGuid(),
                             RoleId = managerRoleId,
                             NavigationMenuId = new Guid("17a08e70-686e-4049-9746-b565a0fe8924")
                         },
                         new RoleMenuPermission
                         {
                             Id = Guid.NewGuid(),
                             RoleId = superAdminRoleId,
                             NavigationMenuId = new Guid("17a08e70-686e-4049-9746-b565a0fe8924")
                         }
                         //
                         ,
                         new RoleMenuPermission
                         {
                             Id = Guid.NewGuid(),
                             RoleId = adminRoleId,
                             NavigationMenuId = new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7")
                         },
                        new RoleMenuPermission
                        {
                            Id = Guid.NewGuid(),
                            RoleId = librarianRoleId,
                            NavigationMenuId = new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7")
                        },
                         new RoleMenuPermission
                         {
                             Id = Guid.NewGuid(),
                             RoleId = managerRoleId,
                             NavigationMenuId = new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7")
                         },
                         new RoleMenuPermission
                         {
                             Id = Guid.NewGuid(),
                             RoleId = superAdminRoleId,
                             NavigationMenuId = new Guid("5f866173-a55c-4ac8-93df-18aa625de1d7")
                         },
                           new RoleMenuPermission
                           {
                               Id = Guid.NewGuid(),
                               RoleId = librarianRoleId,
                               NavigationMenuId = new Guid("d3208527-a5f5-4e33-88c8-1a30af0e97e1")
                           },
                            new RoleMenuPermission
                            {
                                Id = Guid.NewGuid(),
                                RoleId = librarianRoleId,
                                NavigationMenuId = new Guid("936a8edd-a127-4344-af9a-93ed9a32537f")
                            },
                               new RoleMenuPermission
                               {
                                   Id = Guid.NewGuid(),
                                   RoleId = librarianRoleId,
                                   NavigationMenuId = new Guid("2eb40a9c-2565-4c2c-a832-feca78f4603b")
                               },
                                  new RoleMenuPermission
                                  {
                                      Id = Guid.NewGuid(),
                                      RoleId = librarianRoleId,
                                      NavigationMenuId = new Guid("9b96f51e-3d27-4eeb-b491-b6a75d8f0a01")
                                  },
                                     new RoleMenuPermission
                                     {
                                         Id = Guid.NewGuid(),
                                         RoleId = librarianRoleId,
                                         NavigationMenuId = new Guid("d0c80121-b900-4515-8521-2737468ffa6a")
                                     },
                                        new RoleMenuPermission
                                        {
                                            Id = Guid.NewGuid(),
                                            RoleId = librarianRoleId,
                                            NavigationMenuId = new Guid("092ac460-2e13-4e44-b207-7d44e3f5ca8b")
                                        },
                                          new RoleMenuPermission
                                          {
                                              Id = Guid.NewGuid(),
                                              RoleId = librarianRoleId,
                                              NavigationMenuId = new Guid("f513d8d7-b945-40d2-b679-2c2bbdd24f25")
                                          },
                                            new RoleMenuPermission
                                            {
                                                Id = Guid.NewGuid(),
                                                RoleId = librarianRoleId,
                                                NavigationMenuId = new Guid("10dc9edc-913d-4dda-a6ff-fe9065d575ee")
                                            }






                );



        }
    }
}
