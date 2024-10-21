using AutoMapper;
using SummitShop.API.DTOs;
using SummitShop.Core.Entities;
using SummitShop.Core.Entities.Identity;

namespace SummitShop.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductResponseDTO>()
                .ForMember(destination => destination.ProductType, option => option.MapFrom(source => source.ProductType.Name))
                .ForMember(destination => destination.ProductBrand, option => option.MapFrom(source => source.ProductBrand.Name))
                .ForMember(d => d.PictureURL, o => o.MapFrom<ProductPictureURLResolver>());

            //CreateMap<Address, AddressDTO>()
            //    .ForMember(destination => destination.Street, option => option.MapFrom(source => source.Street))
            //    .ForMember(destination => destination.City, option => option.MapFrom(source => source.City))
            //    .ForMember(destination => destination.Country, option => option.MapFrom(source => source.Country));

            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
