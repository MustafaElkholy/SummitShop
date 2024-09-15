using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummitShop.API.DTOs;
using SummitShop.API.Errors;
using SummitShop.Core.Entities.Identity;
using SummitShop.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace SummitShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService tokenService;

        public AccountController(
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpPost("Login")] // POST:  /api/Account/Login
        public async Task<ActionResult<UserDTO>> Login(UserLoginDTO model)
        {
            // no need to check model validation because we customized the errors in program.cs

            ApplicationUser user;

            if (new EmailAddressAttribute().IsValid(model.UserNameOrEmail))
            {
                user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            }
            if (user is null) return Unauthorized(new APIResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new APIResponse(401));

            return Ok(new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateJWTTokenAsync(user, _userManager)
            });

        }

        [HttpPost("Register")] // POST:  /api/Account/Register
        public async Task<ActionResult<UserDTO>> Register(UserRegisterDTO model)
        {
            var user = new ApplicationUser
            {
                Email = model.EmailAddress,
                UserName = model.EmailAddress.Split('@')[0],
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new APIResponse(400));

            return Ok(new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateJWTTokenAsync(user, _userManager)
            });
        }


    }
}
