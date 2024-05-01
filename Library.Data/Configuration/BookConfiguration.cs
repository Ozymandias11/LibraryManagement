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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);


            builder.Property(b => b.Title).IsRequired();

            builder.Property(b => b.PublishedYear).IsRequired();

            builder.Property(b => b.Edition).IsRequired();

            builder.HasMany(b => b.Authors)
                .WithOne(ba => ba.Book)
                .HasForeignKey(ba => ba.BookId);

            builder.HasMany(b => b.Categories)
                .WithOne(bc => bc.Book)
                .HasForeignKey(bc => bc.BookId);

            builder.HasMany(b => b.Publishers)
                .WithOne(bp => bp.Book)
                .HasForeignKey(bp => bp.BookId);

            builder.HasMany(b => b.Copyrights)
                .WithOne(bc => bc.OriginalBook)
                .HasForeignKey(bc => bc.OriginaBookId);

            

        }
    }
}
