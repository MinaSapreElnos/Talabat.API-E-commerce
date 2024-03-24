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
    internal class DelivaryMethodConfigrations : IEntityTypeConfiguration<DelivaryMethod>
    {
        public void Configure(EntityTypeBuilder<DelivaryMethod> builder)
        {
            builder.Property(Order => Order.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
