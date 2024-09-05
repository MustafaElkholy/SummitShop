using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummitShop.API.Errors;

namespace SummitShop.API.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new APIResponse(code));

        }
    }
}
