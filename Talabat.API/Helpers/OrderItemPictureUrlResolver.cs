using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Models.OrderAggragation;

namespace Talabat.API.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.ProductUrl))
                return $"{_configuration["APiBaseUrl"]}/{source.Product.ProductUrl}";

            return string.Empty;

        }
    }
}
