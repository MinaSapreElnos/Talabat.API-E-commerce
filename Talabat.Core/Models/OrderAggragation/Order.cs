using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.OrderAggragation
{
    public class Order : BaseEntity 
    {
        public Order() 
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DelivaryMethod dlivaryMethod, ICollection<OrderItem> items, decimal subTotal ,string PaymentIntendId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DlivaryMethod = dlivaryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = PaymentIntendId;
        }
         
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public DelivaryMethod? DlivaryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }



        // Consederd of Drevet itter //
        public decimal GetTotal()
            => SubTotal + DlivaryMethod.Cost;

        public string PaymentIntentId { get; set; }


    }
}
