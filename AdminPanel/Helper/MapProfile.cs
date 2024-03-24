using AdminPanel.Models;
using AutoMapper;
using Stripe;
using Talabat.API.DTOs;
using Talabat.Core.Models;

namespace AdminPanel.Helper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
			CreateMap<ProductToReturnDto, Talabat.Core.Models.Product>().ReverseMap();

			CreateMap<ProductViewModel, Talabat.Core.Models.Product>().ReverseMap();




			//CreateMap<ProductToReturnDto, Product>(); 

		}
	}
}
