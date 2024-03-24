using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;
using Talabat.Core.Models.OrderAggragation;
using Talabat.Core.Services.Contract;
using Talabat.Core.Spacifications.OrderSpac;

namespace Talabat.Services
{
    public class OrderService : IOrderService 
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        

        public OrderService( IBasketRepository basketRepository ,IUnitOfWork _unitOfWork)
        {
            this.basketRepository = basketRepository;

            unitOfWork = _unitOfWork;
        }

        public async Task<Order> CeataOrderAsync(string BuyerEmail, string BasketId, int DelivaryMethodId, Address ShippingAdress)
        {
            var Basket = await basketRepository.GetBasketAsync(BasketId);

            var OrderItems = new List<OrderItem>();

            if (Basket?.Items?.Count() > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await unitOfWork.Repository<Product>().GetAsync(item.Id);

                    var ProductItemOrder = new ProductItemOrder(item.Id, Product.Name, Product.PictureUrl);

                    var OrderItem = new OrderItem(ProductItemOrder, Product.Price, item.Quantity);

                    OrderItems.Add(OrderItem);
                }
            }



                var subTotal = OrderItems.Sum(OrderItem => OrderItem.Price * OrderItem.Quantity);

                var DelivaryMethod = await unitOfWork.Repository<DelivaryMethod>().GetAsync(DelivaryMethodId);
        
                var Order = new Order(BuyerEmail, ShippingAdress, DelivaryMethod, OrderItems, subTotal ,Basket.PaymentIntentId);

                await unitOfWork.Repository<Order>().AddAcync(Order);

                var Result = await unitOfWork.CompleteAcync();

                if (Result<= 0) { return null; }

                return Order;
        }

    

        public async Task<IEnumerable<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var Spac = new OrderSpacification( BuyerEmail);

            var orders = await unitOfWork.Repository<Order>().GetAllWithSpacAsync(Spac);


            return orders;


        }





        public  async Task<Order?> GetOrderByIdForUserAsync(int  OrderId, string BuyerEmail)
        {
            var OrderRepo = unitOfWork.Repository<Order>();

            var Spac = new OrderSpacification(OrderId, BuyerEmail);

            var Order = await OrderRepo.GetWithSpacAsync(Spac);

            return Order;

        }

        public Task<IEnumerable<DelivaryMethod>> GetDelivaryMethodsAcync()
        {
            var DelevaryMathodRepo =unitOfWork.Repository<DelivaryMethod>();    

            var DelevaryMethod = DelevaryMathodRepo.GetAllAsync();

            return DelevaryMethod ;
        }
    }
}
