using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Extensions
{
    public static class UserManager_EX
    {

        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return  userManager.Users.Include(x=>x.Address).SingleOrDefault(x=>x.Email== email);
        }


        
    }
}
