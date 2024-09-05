using AutoMapper;
using SummitShop.API.DTOs;
using SummitShop.Core.Entities;

namespace SummitShop.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductResponseDTO>()
                .ForMember(destination=>destination.ProductType, option => option.MapFrom(source=>source.ProductType.Name))
                .ForMember(destination=>destination.ProductBrand, option => option.MapFrom(source=>source.ProductBrand.Name))
                .ForMember(d=>d.PictureURL, o=>o.MapFrom<ProductPictureURLResolver>()); 
        }
    }
}
