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
    public class BookCopyShelfConfiguration : IEntityTypeConfiguration<BookCopyShelf>
    {
        public void Configure(EntityTypeBuilder<BookCopyShelf> builder)
        {
            builder.HasKey(bcs => new { bcs.BookCopyId, bcs.ShelfId , bcs.RoomId});
        }
    }
}
