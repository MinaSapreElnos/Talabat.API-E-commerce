using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.Core.Spacifications.OrderSpac
{
    public class OrderSpacification : BaseSpacification<Order>
    {

        public OrderSpacification(string BuyerEmail) : base(O=>O.BuyerEmail== BuyerEmail)
        {
            Includes.Add(O => O.DlivaryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(O => O.OrderDate);
        }


        public OrderSpacification(int OrderId, string BuyerEmail):base(

             O=>O.Id == OrderId && O.BuyerEmail == BuyerEmail
            )
        {
            Includes.Add(O => O.Items);

            Includes.Add(O => O.DlivaryMethod);

        }
    }
}
