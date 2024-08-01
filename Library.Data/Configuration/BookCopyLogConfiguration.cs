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
    public class BookCopyLogConfiguration : IEntityTypeConfiguration<BookCopyLog>
    {
        public void Configure(EntityTypeBuilder<BookCopyLog> builder)
        {
            builder.HasKey(bcl => bcl.LogId);

            builder.Property(bcl => bcl.Edition)
                .IsRequired();

            builder.Property(bcl => bcl.State)
                .IsRequired();

            builder.Property(bcl => bcl.Message)
                .IsRequired();  

            builder.Property(bcl => bcl.QuantityModified)
                .IsRequired();

            builder.Property(bcl => bcl.TimeStamp)
                .IsRequired();

            builder
                .HasOne(bcl => bcl.OriginalBook)
                .WithMany(b => b.BookCopyLogs)
                .HasForeignKey(bcl => bcl.OriginalBookId);

            builder
                .HasOne(bcl => bcl.Publisher)
                .WithMany(p => p.BookCopyLogs)
                .HasForeignKey(bcl => bcl.PublishersId);



        }
    }
}
