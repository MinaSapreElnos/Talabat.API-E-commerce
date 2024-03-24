using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Models.Identity;

namespace AdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController( UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Users = await userManager.Users.Select( U => new UserViewModel
            {
                Id = U.Id,

                UserName = U.UserName,

                Email = U.Email,

                DisplayName = U.DisplayName,
                Roles = userManager.GetRolesAsync(U).Result
            }).ToListAsync() ;

            return View(Users);
        }

        // Edit Get //

        [HttpGet]

        public async Task<IActionResult> Edit (string id)
        {
            var User = await userManager.FindByIdAsync(id);

            var AllRole = await roleManager.Roles.ToListAsync();

            var ViewModel = new UserRoleViewModel
            {
                UserId = User.Id,

                UserName = User.DisplayName,

                Roles = AllRole.Select(u => new RoleViewModel
                {
                    Id = u.Id,
                    Name = u.Name,

                    isSelected = userManager.IsInRoleAsync(User, u.Name).Result
                }).ToList()
            };
            return View(ViewModel);
        }


        // Edit Post // 
        [HttpPost]
        public async Task<IActionResult > Edit( UserRoleViewModel model)
        {
            var User = await userManager.FindByIdAsync(model.UserId);

            var UserRole = await userManager.GetRolesAsync(User);
            
            foreach (var role in model.Roles)
            {
                if(UserRole.Any(R => R == role.Name) && !role.isSelected)
                {
                    await userManager.RemoveFromRoleAsync(User,role.Name);
                }
                if (!UserRole.Any(R =>R == role.Name) && role.isSelected)
                {
                    await userManager.AddToRoleAsync(User,role.Name);   
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
