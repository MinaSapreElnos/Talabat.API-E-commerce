using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Spacifications.ProductSpac
{
    public class ProductSpacificationParams 
    {
        private int pageSize =5; 

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize= value > 10?  10 : value  ; } 
        }
        public int PageIndex { get; set; } 
        public string? Sort { get; set; }

        public string? Search { get; set; } 
        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }  



    }
}
