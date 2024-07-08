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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {

            builder.HasKey(r => r.ReservationId);

            builder.Property(r => r.CheckoutTime).IsRequired();
            builder.Property(r => r.SupposedReturnDate).IsRequired();
            builder.Property(r => r.EmployeeId).IsRequired();


            builder.HasMany(r => r.ReservationItems)
                .WithOne(ri => ri.Reservation)
                .HasForeignKey(ri => ri.ReservationId);

            builder.Property(r => r.ActualReturnDate).IsRequired(false);

            // generated column IsLate
            builder.Property(r => r.IsLate)
                    .HasComputedColumnSql("CASE WHEN [ActualReturnDate] IS NULL THEN CAST(NULL AS BIT) " +
                    "ELSE CAST(CASE WHEN [ActualReturnDate] > [SupposedReturnDate] THEN 1 ELSE 0 END AS BIT) END");





        }
    }
}
