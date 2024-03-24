using Talabat.Core.Models.OrderAggragation;

namespace Talabat.API.DTOs
{
    public class OrderToReturnDTO 
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; } 

        public Address ShippingAddress { get; set; }

        public string DlivaryMethodName { get; set; }

        public decimal DlivaryMethodCost { get; set; } 
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public decimal SubTotal { get; set; }
        
        public decimal Total { get; set; } 

        public string? PaymentIntentId { get; set; }
    }
}
