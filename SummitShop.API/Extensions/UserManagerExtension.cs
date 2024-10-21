using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SummitShop.Core.Entities.Identity;
using System.Security.Claims;

namespace SummitShop.API.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser?>FindUserWithAddressAsync
                            (this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
