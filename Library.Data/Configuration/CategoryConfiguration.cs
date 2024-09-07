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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);


            builder.HasMany(b => b.BookCategories)
                .WithOne(bc => bc.Category)
                .HasForeignKey(bc => bc.CategoryId);

            builder.HasMany(c => c.Titles)
                .WithOne(ct => ct.Category)
                .HasForeignKey(ct => ct.CategoryId);    


        }
    }
}
