using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummitShop.API.DTOs;
using SummitShop.API.Errors;
using SummitShop.API.Extensions;
using SummitShop.Core.Entities.Identity;
using SummitShop.Core.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SummitShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ITokenService tokenService,
                                IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
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
            if (CheckEmailExist(model.EmailAddress).Result.Value)
            {
                return BadRequest(new APIValidationErrorResponse()
                {
                    Errors = new string[] { "This Email Is Already Exists" }
                });
            }

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

        [HttpGet("GetCurrentUser")] // Get the current User that sent this request
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(userEmail);

            return Ok(new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateJWTTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("GetUserAddress")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);

            //var address = new AddressDTO
            //{
            //    City = user.Address.City,
            //    Country = user.Address.Country,
            //    Street = user.Address.Street
            //};

            var address = mapper.Map<Address, AddressDTO>(user.Address);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO updatedAddress)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            if (user == null)
            {
                return NotFound(new APIResponse(404, "User not found"));
            }

            if (user.Address == null)
            {
                user.Address = mapper.Map<AddressDTO, Address>(updatedAddress);
            }
            else
            {
                updatedAddress.Id = user.Address.Id; 
                mapper.Map(updatedAddress, user.Address);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new APIResponse(400));

            return Ok(updatedAddress);
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
