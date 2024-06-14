using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.HasKey(bc => bc.BookCopyId);

            builder.Property(bc => bc.NumberOfPages).IsRequired();

            builder.Property(bc => bc.Status).IsRequired();

            builder.Property(bc => bc.Edition).IsRequired();

           
           

            builder.HasMany(bc => bc.Reservations)
                .WithOne(r => r.BookCopy)
                .HasForeignKey(r => r.BookCopyID);

            builder.HasMany(bc => bc.Shelves)
                .WithOne(s => s.BookCopy)
                .HasForeignKey(s => s.BookCopyId);

            builder.HasOne(bc => bc.Publisher)
                .WithMany()
                .HasForeignKey(bc => bc.PublisherId);

           


        }
    }
}
