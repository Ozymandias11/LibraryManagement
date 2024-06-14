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
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.RoomId);

            builder.Property(r => r.Capacity).IsRequired();

            builder.Property(r => r.RoomNumber).IsRequired();   

            builder.HasMany(r => r.Shelves)
                .WithOne(s => s.Room)
                .HasForeignKey(s => s.RoomId);


            builder.HasData(

                    new Room
                    {
                        RoomId = new Guid("4c1d5049-b1cd-4130-999b-ec58b185f59a"),
                        Capacity = 20,
                        RoomNumber = 101
                    },

                   new Room
                   {
                       RoomId = new Guid("f95a0286-27df-4dea-b2e1-1e9e6dac07fd"),
                       Capacity = 30,
                       RoomNumber = 102
                   },

                   new Room
                   {
                       RoomId = new Guid("45f5855a-8bed-4b4f-b42a-979f25f6d3d5"),
                       Capacity = 25,
                       RoomNumber = 201
                   }

                );



        }
    }
}
