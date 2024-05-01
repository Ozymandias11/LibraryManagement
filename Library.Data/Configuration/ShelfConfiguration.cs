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


            builder.HasMany(s => s.Books)
                .WithOne(bcs => bcs.Shelf)
                .HasForeignKey(bcs => new {bcs.RoomId, bcs.ShelfId});


    }
   }
}
