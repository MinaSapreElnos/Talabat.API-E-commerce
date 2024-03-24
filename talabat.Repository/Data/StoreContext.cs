using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.Repository.Data
{
    public class StoreContext :DbContext 
    {

        public StoreContext( DbContextOptions<StoreContext> options): base(options) 
        {
               
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> products { get; set; }

        public DbSet<ProductBrand> productBrands { get; set; }

        public DbSet< ProductCategory > productCategories { get; set; } 

        public DbSet<Order> orders { get; set; }

        public DbSet<OrderItem> orderItems { get; set; }

        public DbSet<DelivaryMethod> delivaryMethods { get; set;} 
        

    }
}
