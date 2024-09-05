using Microsoft.AspNetCore.Mvc;
using SummitShop.API.Errors;
using SummitShop.API.Helpers;
using SummitShop.Core.Repositories;
using SummitShop.Repository.RepositoryImplementation;

namespace SummitShop.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //builder.Services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();

            //Generic Register
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Basket Repository
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));


            //builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
            services.AddAutoMapper(typeof(MappingProfiles));


            // how to configure the response of invalid model state (the validation error)
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(p => p.ErrorMessage).ToList();
                    var validationErrorResponse = new APIValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            return services;
        }
    }
}
