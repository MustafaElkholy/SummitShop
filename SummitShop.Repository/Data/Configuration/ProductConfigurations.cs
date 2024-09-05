using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SummitShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Repository.Data.Configuration
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand).WithMany(b => b.Products);
            builder.HasOne(p => p.ProductType).WithMany(t => t.Products);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.PictureURL).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
