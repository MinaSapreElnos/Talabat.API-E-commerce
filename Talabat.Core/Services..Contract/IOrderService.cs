using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order?> CeataOrderAsync(string BuyerEmail ,string BasketId, int DelivaryMethod , Address ShippingAdress );

        Task<IEnumerable<Order>> GetOrdersForUserAsync(string BuyerEmail);

        Task<Order?> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail);

        Task<IEnumerable<DelivaryMethod>> GetDelivaryMethodsAcync(); 
    }
}
