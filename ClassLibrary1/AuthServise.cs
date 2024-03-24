using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
    public class AuthServise : IAuthServise
    {
        private readonly IConfiguration configuration;

        public AuthServise(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateTokenAcync(AppUser User, UserManager<AppUser> userManager)
        {
            var authClimes = new List<Claim>()
            {
                new Claim (ClaimTypes.GivenName, User.UserName) ,
                new Claim (ClaimTypes.Email, User.Email) ,
            };

            var UserRoles = await userManager.GetRolesAsync(User);

            foreach (var role in UserRoles)
            {
                authClimes.Add(new Claim(ClaimTypes.Role, role));
            }

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecritKey"]));

            var Token = new JwtSecurityToken(

                audience: configuration["Jwt:ValiedAudience"],
                issuer: configuration["Jwt:issuer"],
                expires : DateTime.UtcNow.AddDays(double.Parse(configuration["Jwt:DurationDay"])),
                claims: authClimes,
                signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature) 

            );

            return new JwtSecurityTokenHandler().WriteToken(Token);  


        }
    }
}
