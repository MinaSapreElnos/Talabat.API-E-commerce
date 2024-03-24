using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.IGenericRepository
{
    public interface IBasketRepository 
    {

        Task<CustomerBasket> GetBasketAsync(string BasketId); 

        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
