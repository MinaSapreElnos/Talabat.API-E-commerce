using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.Core.Models.Identity;

namespace AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AdminController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login() 
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var User = await userManager.FindByEmailAsync(loginDto.Email);

            if (User == null)
            {
                ModelState.AddModelError("Email", "Email is in Valid");
            }

            var Result = await signInManager.CheckPasswordSignInAsync(User, loginDto.Password, false);

            if (!Result.Succeeded || !await userManager.IsInRoleAsync(User ,"Admin"))
            {
                ModelState.AddModelError(string.Empty, "Not Authorzed");
                return RedirectToAction(nameof (Login));
            } 
            else
            {
                return RedirectToAction("Index","Home");
            }  
        }

        //logout

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
