using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Spacifications.ProductSpac
{
    public class ProductWithFilltrationForCountSpacification :BaseSpacification<Product> 
    {
        public ProductWithFilltrationForCountSpacification(ProductSpacificationParams SpacPrams) :
             base(P =>
                        (string.IsNullOrEmpty(SpacPrams.Search)||P.Name.ToLower().Contains(SpacPrams.Search.ToLower())) &&
                        (!SpacPrams.BrandId.HasValue || P.BrandId == SpacPrams.BrandId.Value) &&
                        (!SpacPrams.CategoryId.HasValue || P.CategoryId == SpacPrams.CategoryId.Value)
                )
        {
            
        }
    }
}
