using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(m => m.ProductBrand, pt => pt.MapFrom(p => p.ProductBrand.Name))
                .ForMember(m => m.ProductType, pt => pt.MapFrom(p => p.ProductType.Name))
                .ForMember(m => m.PictureUrl, pt => pt.MapFrom<ProductURLResolver>());
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
        }
    }
}
