using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummitShop.API.DTOs;
using SummitShop.API.Errors;
using SummitShop.API.Helpers;
using SummitShop.Core.Entities;
using SummitShop.Core.Repositories;
using SummitShop.Core.Specifications;

namespace SummitShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMapper mapper;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;

        public ProductController(IGenericRepository<Product> productRepo
                                ,IMapper mapper
                                ,IGenericRepository<ProductBrand> brandRepo
                                ,IGenericRepository<ProductType> typeRepo)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
        }

        [ProducesResponseType(typeof(Pagination<ProductResponseDTO>),StatusCodes.Status200OK)]
        [HttpPost]
        //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]
        public async Task<ActionResult<Pagination<ProductResponseDTO>>> GetAllProductsAsync(ProductSpecificationParameters productSpecParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(productSpecParams);

            var productCountSpec = new ProductWithFiltrationForCountSpecification(productSpecParams);

            var products = await productRepo.GetAllWithSpecAsync(spec);
            var productsData = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductResponseDTO>>(products);

            var dataCount = await productRepo.GetCountWithSpecAsync(productCountSpec);

            var response = new Pagination<ProductResponseDTO>
            {
                PageNumber = productSpecParams.PageNumber,
                PageSize = productSpecParams.PageSize,
                Count = dataCount,
                Data = productsData
            };

            return Ok(response);
        }

        [ProducesResponseType(typeof(ProductResponseDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<ProductResponseDTO>>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await productRepo.GetByIdWithSpecAsync(spec);
            if (product is null)
            {
                return NotFound(new APIResponse(404));
            }
            return Ok(mapper.Map<Product, ProductResponseDTO>(product));
        }

        [HttpGet("brands")] // Get: api/Product/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetProductBrands()
        {
            var brands = await brandRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")] // Get: api/Product/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await typeRepo.GetAllAsync();
            return Ok(types);
        }

    }
}
