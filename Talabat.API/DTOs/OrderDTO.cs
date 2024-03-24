using System.ComponentModel.DataAnnotations;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.API.DTOs
{
    public class OrderDTO
    {
        //[Required]
        //public string BuyerEmail { get; set; }

        [Required]
        public string BasketId { get; set;}
        [Required]
        public int DelivaryMathodId { get; set;}
     
        public AddressDTO ShippingAddress { get; set; }

    }
}
