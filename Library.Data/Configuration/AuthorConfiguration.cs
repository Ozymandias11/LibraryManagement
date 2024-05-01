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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.AuthorId);

            builder.Property(a => a.FirstName).IsRequired();
            builder.Property(a => a.LastName).IsRequired();

            builder.HasMany(a => a.BooksWritten)
                .WithOne(ba => ba.Author)
                .HasForeignKey(ba => ba.AuthorID);
                


        }
    }
}
