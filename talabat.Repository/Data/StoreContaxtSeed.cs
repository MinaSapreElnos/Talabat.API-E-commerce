using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.Repository.Data
{
    public class StoreContaxtSeed
    {
        public static async Task SeedAsync(StoreContext _DbContaxt)
        {
            if (_DbContaxt.productBrands.Count() == 0)
            {
                var BrendsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");

                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrendsData);

                if (Brands?.Count() > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        _DbContaxt.Set<ProductBrand>().Add(Brand);
                    }
                    _DbContaxt.SaveChanges();
                }
            }

            //Seed Category //

            if (_DbContaxt.productCategories.Count() == 0)
            {
                var CategoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");

                var Categorys = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);

                if (Categorys?.Count() > 0)
                {
                    foreach (var Category in Categorys)
                    {
                        _DbContaxt.Set<ProductCategory>().Add(Category);
                    }
                    _DbContaxt.SaveChanges();
                }
            }


            //Seed Product//

            if (_DbContaxt.products.Count() == 0)
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        _DbContaxt.Set<Product>().Add(product);
                    }
                    _DbContaxt.SaveChanges();
                }
            }


            //Seed DelivaryMethods //


            if (_DbContaxt.delivaryMethods.Count() == 0)
            {
                var delivaryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");

                var DelivaryMethods = JsonSerializer.Deserialize<List<DelivaryMethod>>(delivaryMethodsData);

                if (DelivaryMethods?.Count() > 0)
                {
                    foreach (var DelivaryMethod in DelivaryMethods)
                    {
                        _DbContaxt.Set<DelivaryMethod>().Add(DelivaryMethod);
                    }
                    await _DbContaxt.SaveChangesAsync();
                }
            }


        }

    }
}
