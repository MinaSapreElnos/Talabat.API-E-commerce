using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Models.OrderAggragation;
//using Address = Talabat.Core.Models.Identity.Address;

namespace Talabat.API.Helpers
{
    public class MapingProfile : Profile
    {
        public MapingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
              .ForMember(S => S.Brand, O => O.MapFrom(S => S.Brand.Name))
              .ForMember(S => S.Category, O => O.MapFrom(S => S.Category.Name))
              .ForMember(S => S.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());



            CreateMap<AddressDTO,Talabat.Core.Models.OrderAggragation.Address>();
            //.ForMember(S=>S.FristName,S=>S.MapFrom(AD=>AD.FristName))
            // .ForMember(S => S.LastName, S => S.MapFrom(AD => AD.LastName))
            // .ForMember(S => S.Street, S => S.MapFrom(AD => AD.Street))
            // .ForMember(S => S.City, S => S.MapFrom(AD => AD.City))
            // .ForMember(S => S.Country, S => S.MapFrom(AD => AD.Country));


            CreateMap<AddressDTO, Talabat.Core.Models.Identity.Address>()
                .ForMember(S => S.FName, S => S.MapFrom(AD => AD.FristName))
                .ForMember(S => S.LName, S => S.MapFrom(S => S.LastName))
                .ForMember(S => S.Street, S => S.MapFrom(AD => AD.Street))
                .ForMember(S => S.City, S => S.MapFrom(AD => AD.City))
                .ForMember(S => S.Country, S => S.MapFrom(AD => AD.Country)).ReverseMap();




            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(D => D.DlivaryMethodName, O => O.MapFrom(S => S.DlivaryMethod.ShortName))
                .ForMember(D => D.DlivaryMethodCost, O => O.MapFrom(S => S.DlivaryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductUrl, O => O.MapFrom(S => S.Product.ProductUrl))
                
                .ForMember(D=>D.ProductUrl,O=>O.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
