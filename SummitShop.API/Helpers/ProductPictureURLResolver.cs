using AutoMapper;
using SummitShop.API.DTOs;
using SummitShop.Core.Entities;

namespace SummitShop.API.Helpers
{

    public class ProductPictureURLResolver : IValueResolver<Product, ProductResponseDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureURLResolver(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }  
        public string Resolve(Product source, ProductResponseDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
            {
                return $"{configuration["APIBaseURL"]}{source.PictureURL}";

            }
            return string.Empty;
        }
    }
}
