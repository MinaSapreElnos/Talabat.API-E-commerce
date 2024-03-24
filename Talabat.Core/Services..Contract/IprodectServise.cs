using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Spacifications.ProductSpac;

namespace Talabat.Core.Services.Contract
{
    public interface IprodectServise 
    {
        Task<IEnumerable<Product>> GetProductsAsync(ProductSpacificationParams SpacPrams);


        Task<Product> GetProductAsync(int ProductId);
        
        Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync();

        Task<int> GetCountAsync(ProductSpacificationParams SpacPrams);

        Task<ProductBrand> GetBrandsAcync();

        Task<ProductCategory> GetCategoriesAcync();

    }
}
