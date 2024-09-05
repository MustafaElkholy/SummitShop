using SummitShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
    {
        // this constructor used to get all products
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters productSpecParams)
            : base(p =>
                (string.IsNullOrEmpty(productSpecParams.SearchBy) || p.Name.ToLower().Contains(productSpecParams.SearchBy.ToLower())) &&
                (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId)
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            AddOrderBy(p => p.Name);


            if (!string.IsNullOrWhiteSpace(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;


                }
            }
            if (productSpecParams.PageSize > 0 && productSpecParams.PageNumber > 0)
            {
                ApplyPagination(productSpecParams.PageSize * (productSpecParams.PageNumber - 1), productSpecParams.PageSize);

            }
        }

        // This constructor is used for get a specific product
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {

            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }




    }
}
