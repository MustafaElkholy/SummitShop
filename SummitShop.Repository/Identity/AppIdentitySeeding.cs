using Microsoft.AspNetCore.Identity;
using SummitShop.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Repository.Identity
{
    public static class AppIdentitySeeding
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            try
            {
                if (!userManager.Users.Any())
                {
                    var user = new ApplicationUser()
                    {
                        DisplayName = "Mustafa Elkholy",
                        Email = "mustafaelkholy7@gmail.com",
                        UserName = "elkholy74",
                        PhoneNumber = "01019185580",
                    };

                    var result = await userManager.CreateAsync(user, "Mustafa123456!");

                    if (!result.Succeeded)
                    {
                        // Log or inspect the errors here
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // Log the exception or handle it accordingly
                throw;
            }
        }
    }
}
