using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Library.Data.Configuration
{
    public class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            
            builder.HasKey(s => new {s.RoomId, s.ShelfId});

            builder.Property(s => s.MaxCapacity).IsRequired();

            builder.Property(s => s.ShelfNumber).IsRequired();

            builder.HasMany(s => s.Books)
                .WithOne(bcs => bcs.Shelf)
                .HasForeignKey(bcs => new {bcs.RoomId, bcs.ShelfId});

            builder.HasData(
                 
                  new Shelf {

                    ShelfId = new Guid("8a491b2f-b39f-4957-abc6-58db09042395") ,
                    RoomId = new Guid("4c1d5049-b1cd-4130-999b-ec58b185f59a"),
                    ShelfNumber = 1,
                    MaxCapacity = 100
                    
                    },

                  new Shelf
                  {

                      ShelfId = new Guid("a30ec77f-2a39-466d-a9ce-596d6e081075"),
                      RoomId = new Guid("4c1d5049-b1cd-4130-999b-ec58b185f59a"),
                      ShelfNumber = 2,
                      MaxCapacity = 100

                  },

                  new Shelf
                  {
                      ShelfId = new Guid("94e8259a-fbd7-44c8-ae93-c0772df00f8f"),
                      RoomId = new Guid("f95a0286-27df-4dea-b2e1-1e9e6dac07fd"),
                      ShelfNumber = 1,
                      MaxCapacity = 100
                  },


                  new Shelf
                  {
                      ShelfId = new Guid("d3595162-093a-4fc8-b046-ad38fc481872"),
                      RoomId = new Guid("45f5855a-8bed-4b4f-b42a-979f25f6d3d5"),
                      ShelfNumber = 1,
                      MaxCapacity = 100
                  }


                );


         


    }
   }
}
