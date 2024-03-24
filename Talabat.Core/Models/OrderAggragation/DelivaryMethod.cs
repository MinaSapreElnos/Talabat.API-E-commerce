using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.OrderAggragation
{
    public class DelivaryMethod :BaseEntity
    {
        public DelivaryMethod()
        {
            
        }


        public DelivaryMethod(string shortName, string description, decimal cost, string delivarayTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = delivarayTime;
        }

        public string ShortName { get; set; }
        
        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string DeliveryTime { get; set; }

        

    }
}
