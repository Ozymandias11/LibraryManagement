﻿using Library.Model.Models;
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

           

            builder.HasOne(bc => bc.Publisher)
                .WithMany()
                .HasForeignKey(bc => bc.PublisherId);

            builder.HasMany(bc => bc.ReservationItems)
                .WithOne(ri => ri.BookCopy)
                .HasForeignKey(ri => ri.BookCopyID);

            builder.HasOne(bc => bc.BookCopyShelf)
                .WithMany(bcs => bcs.BookCopies)
                .HasForeignKey(bc => bc.BookCopyShelfId)
                .OnDelete(DeleteBehavior.SetNull);
                

           


        }
    }
}
