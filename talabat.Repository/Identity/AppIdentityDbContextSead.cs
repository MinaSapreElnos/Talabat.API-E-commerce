using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSead
    {
        public static async Task SeadUserAsync(UserManager<AppUser> _userManager)
        {


            if(_userManager.Users.Count() == 0)
            {
                var User = new AppUser()
                {
                    
                    DisplayName = "Mina Sapre",

                    Email = "Mina.Sapre@yahoo.com",

                    UserName = "Mina.sapre",

                    PhoneNumber = "01275109248"
                };

                await _userManager.CreateAsync(User ,"Pa$$w0rd");

            }



        }
    }
}
