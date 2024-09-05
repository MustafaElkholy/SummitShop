using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummitShop.API.Errors;
using SummitShop.Core.Entities;
using SummitShop.Core.Repositories;

namespace SummitShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            this.basketRepo = basketRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket = await basketRepo.GetBasketAsync(id);

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasket basket)
        {
            var createdOrUpdatedBasket = await basketRepo.UpdateBasketAsync(basket);
            if (createdOrUpdatedBasket is null) return BadRequest(new APIResponse(400));
            return Ok(createdOrUpdatedBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCustomerBasket(string id)
        {
            return await basketRepo.DeleteBasketAsync(id);
        }

    }
}
