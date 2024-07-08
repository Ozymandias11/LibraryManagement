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
    public class ReservationItemConfiguration : IEntityTypeConfiguration<ReservationItem>
    {
        public void Configure(EntityTypeBuilder<ReservationItem> builder)
        {
            builder.HasKey(ri => ri.ReservationItemId);
            builder.Property(ri => ri.ReservationId).IsRequired();
            builder.Property(ri => ri.BookCopyID).IsRequired();

            builder.HasOne(ri => ri.Reservation)
                .WithMany(r => r.ReservationItems)
                .HasForeignKey(ri => ri.ReservationId);

            builder.HasOne(ri => ri.BookCopy)
                .WithMany(bc => bc.ReservationItems)
                .HasForeignKey(ri => ri.BookCopyID);
        }
    }
}
