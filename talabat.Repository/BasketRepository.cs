using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _DataBase;

        public BasketRepository(IConnectionMultiplexer Redis) 
        {
            _DataBase = Redis.GetDatabase();
        }

        public Task<bool> DeleteBasketAsync(string BasketId)
        {
            return _DataBase.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var Basket = await _DataBase.StringGetAsync(Id);

            return Basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);

        }


        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
          var CreatedOrUpdated   =   await _DataBase.StringSetAsync(basket.Id,JsonSerializer.Serialize( basket),TimeSpan.FromDays(30));


            if (CreatedOrUpdated is false) return null;

            return await GetBasketAsync(basket.Id);  
        }


    }
}
