using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.Repository.Data.Configritions
{
    internal class OrderConfigritions : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(O => O.Status).
                HasConversion(
                        Ostatus => Ostatus.ToString(),

                        Ostatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), Ostatus)
                );


            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

            builder.HasOne(O => O.DlivaryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
