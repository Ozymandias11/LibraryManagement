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
    public class CategoryTitleConfiguration : IEntityTypeConfiguration<CategoryTitle>
    {
        public void Configure(EntityTypeBuilder<CategoryTitle> builder)
        {
            builder.HasKey(ct => ct.CatgeoryTitleId);

            builder.Property(ct => ct.Title).IsRequired();

            builder.Property(ct => ct.Language).IsRequired();


        }
    }
}
