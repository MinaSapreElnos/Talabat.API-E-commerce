using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.Core;
using Talabat.Core.Models.OrderAggragation;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
    /*(AuthenticationSchemes = "Bearer")*/
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService; 
        private readonly IMapper mapper;
       

        public OrderController(IOrderService orderService, IMapper mapper )
        {
            this.orderService = orderService;
            this.mapper = mapper;
            
        }


        [HttpPost]

        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var Address = mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);

            var Order = await orderService.CeataOrderAsync(BuyerEmail, orderDTO.BasketId, orderDTO.DelivaryMathodId, Address);

            if (Order == null)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<Order,OrderToReturnDTO>(Order));
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("ForUser")]   

        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetOrderFromUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var Orders = await orderService.GetOrdersForUserAsync(BuyerEmail);

            return Ok(mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(Orders)); 
        }



        [HttpGet("{Id}")] 

        public async Task<ActionResult<Order>> GetOrderForUser(int Id )
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);

            var Order = await orderService.GetOrderByIdForUserAsync(Id, UserEmail);

            if(Order == null) { return NotFound(); }

            return Ok(mapper.Map<Order,OrderToReturnDTO>(Order));

        }

        [HttpGet ("DelivaryMathods")]
        public async Task<ActionResult<IEnumerable<DelivaryMethod>>> GetDelivaryMethodS()
        {
            var DelivaryMethods = await orderService.GetDelivaryMethodsAcync();

            return Ok(DelivaryMethods); 
        }
    }
}
