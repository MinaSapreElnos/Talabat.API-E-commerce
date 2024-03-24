using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.Repository.Data.Configritions
{
    internal class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OrderItem => OrderItem.Product, OrderItem => OrderItem.WithOwner());

            builder.Property(OrderItem => OrderItem.Price)
                .HasColumnType("decimal(18,2)");   
        }
    }
}
