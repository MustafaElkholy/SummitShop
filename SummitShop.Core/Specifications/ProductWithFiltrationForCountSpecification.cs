using SummitShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Core.Specifications
{
    public class ProductWithFiltrationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltrationForCountSpecification(ProductSpecificationParameters productSpecParams)
             : base(p =>
                (string.IsNullOrEmpty(productSpecParams.SearchBy) || p.Name.ToLower().Contains(productSpecParams.SearchBy.ToLower())) &&
                (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId)
            )
        {
                
        }
    }
}
