using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.Core.Models.Identity;

namespace Talabat.API.Extentions
{
    public static class UserMangerExtention
    {

        public static async Task<AppUser?> FindUserWithAdressAsync(this UserManager<AppUser> userManager ,ClaimsPrincipal User)
        {

            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await   userManager.Users.Include(U=>U.Address).SingleOrDefaultAsync(U=>U.Email== Email);

            return  user;

        }

        

    }
}
