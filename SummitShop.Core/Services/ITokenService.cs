using Microsoft.AspNetCore.Identity;
using SummitShop.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateJWTTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
