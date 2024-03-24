using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Repository.Data.Configritions
{
    internal class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                .IsRequired();

            builder.Property(P => P.Description).IsRequired();

            builder.Property(P => P.PictureUrl).IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal (18,2)");


            builder.HasOne(P => P.Brand)
                .WithMany();

            builder.HasOne(P => P.Category)
               .WithMany();
        }
    }
}
