using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(m=>m.ProductBrand,pt=>pt.MapFrom(p=>p.ProductBrand.Name))
                .ForMember(m=>m.ProductType,pt=>pt.MapFrom(p=>p.ProductType.Name))
                .ForMember(m=>m.PictureUrl,pt=>pt.MapFrom<ProductURLResolver>())
               ;
        }
    }
}
