using SummitShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SummitShop.Repository.Data
{
    public static class ContextSeedData
    {
        public static async Task SeedDataAsync(SummitDbContext context)
        {
            if (!context.ProductBrands.Any())
            {
                // Seed Brand
                var brandData = File.ReadAllText("../SummitShop.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await context.ProductBrands.AddAsync(brand);
                    }
                }
                await context.SaveChangesAsync();

            }

            if (!context.ProductTypes.Any())
            {
                // Seed Types
                var typesData = File.ReadAllText("../SummitShop.Repository/Data/DataSeeding/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await context.ProductTypes.AddAsync(type);
                    }
                }
                await context.SaveChangesAsync();

            }

            if (!context.Products.Any())
            {
                // Seed Products

                var productsData = File.ReadAllText("../SummitShop.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await context.Products.AddAsync(product);
                    }
                }
                await context.SaveChangesAsync();

            }


        }
    }
}
