using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.Core
{
    public interface IUnitOfWork : IDisposable
    {

        //public IGenericRepository<Product> ProductRepo { get; set; }

        //public IGenericRepository<ProductBrand> ProductBrandRepo { get; set; }

        //public IGenericRepository<DelivaryMethod> DelivaryMethodRepo { get; set; } 

        //public IGenericRepository<OrderItem> OrderItemRepo { get; set; }

        //public IGenericRepository<Order> OrderRepo { get; set; }
         

        IGenericRepository<TEntity> Repository <TEntity>() where TEntity : BaseEntity;



        Task<int> CompleteAcync();


    }
}
