using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Services.Contract;
using Talabat.Core.Spacifications.ProductSpac;

namespace Talabat.Services
{
    public class ProdectServise : IprodectServise
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdectServise(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ProductSpacificationParams SpacPrams)
        {
            var Spac = new ProductWithBrabdEndCategrySpacification(SpacPrams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpacAsync(Spac);

            return products;
        }


        public async Task<Product> GetProductAsync(int ProductId)
        {
            var Spac = new ProductWithBrabdEndCategrySpacification(ProductId);
            var Product = await _unitOfWork.Repository<Product>().GetWithSpacAsync(Spac);

            return Product;
        }

        public Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(ProductSpacificationParams SpacPrams)
        {
            var ProductCount = new ProductWithFilltrationForCountSpacification(SpacPrams);

            var Count = await _unitOfWork.Repository<Product>().GetCount(ProductCount);

            return Count;
        }

        public async Task<ProductBrand> GetBrandsAcync()
        {
            var Brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

            return (ProductBrand) Brands;
        }

        public async Task<ProductCategory> GetCategoriesAcync()
        {
            var Categorys = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

            return (ProductCategory) Categorys;
        }
    }
}
