using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models
{
    public class CustomerBasket 
    {
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();   

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }

        public int? DelivieryMethodId { get; set; } 

        public decimal SippingPrice { get; set; }  

        public CustomerBasket(string _id) 
        {
            Id = _id;
            //Items = new List<BasketItem>();
        }

        public CustomerBasket()
        {
            
        }
    }
}
