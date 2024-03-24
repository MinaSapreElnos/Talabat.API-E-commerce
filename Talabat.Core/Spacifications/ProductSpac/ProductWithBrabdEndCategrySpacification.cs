using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Spacifications.ProductSpac
{
    public class ProductWithBrabdEndCategrySpacification : BaseSpacification<Product>
    {
        public ProductWithBrabdEndCategrySpacification(ProductSpacificationParams SpacPrams) :
            base(P =>
                        (string.IsNullOrEmpty(SpacPrams.Search)||P.Name.ToLower().Contains(SpacPrams.Search.ToLower())) &&
                        (!SpacPrams.BrandId.HasValue || P.BrandId == SpacPrams. BrandId.Value) &&
                        (!SpacPrams.CategoryId.HasValue || P.CategoryId == SpacPrams.CategoryId.Value)
                )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);


            if (!string.IsNullOrEmpty(SpacPrams.Sort))
            {
                switch (SpacPrams.Sort)
                {
                    case "PriceAse":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyPagination( (SpacPrams.PageIndex - 1) * SpacPrams.PageSize, SpacPrams.PageSize);


        }

        public ProductWithBrabdEndCategrySpacification(int id) : base(p => p.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
