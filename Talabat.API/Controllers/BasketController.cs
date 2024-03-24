using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;

        public BasketController( IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var Basket = await basketRepository.GetBasketAsync(id);

            return Ok(Basket ?? new CustomerBasket(id)); 
        }


        [HttpPost("CreateBasket")]


        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        { 
            var CreatedOrUpdated = await basketRepository.UpdateBasketAsync(basket);
            if (CreatedOrUpdated is null) return BadRequest(); 

            return Ok(CreatedOrUpdated);
        }



        [HttpDelete]
        public async Task Delete(string id)
        {
            await basketRepository.DeleteBasketAsync(id); 

        }
    }
}

//https://localhost:7087/images/products/sb-ang1//