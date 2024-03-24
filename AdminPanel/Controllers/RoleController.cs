using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }


        public async Task< IActionResult > Index()
        {
            var Roles = await roleManager.Roles.ToListAsync();

            return View(Roles);
        }


        // Action Add Role //

        [HttpPost]
        public async Task<IActionResult> Create( RoleFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                var RoleExsist = await roleManager.RoleExistsAsync(model.Name);
                if (!RoleExsist)
                {
                    await roleManager.CreateAsync(new IdentityRole(model.Name));

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error","Role is Exist !!");
                    return View("index" ,await roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction(nameof(Index));
        }


        // Delete Role Action //

        public async Task<IActionResult> Delete( string Id)
        {
            var Role = await roleManager.FindByIdAsync(Id);

            await roleManager.DeleteAsync(Role);

            return RedirectToAction(nameof(Index));
        }


        // Edit 
        //Get

        public async Task<IActionResult> Edit(string Id)
        {
            var Role = await roleManager.FindByIdAsync(Id);

            var MappedRole = new RoleViewModel()
            {
                Name = Role.Name,
            };

            return View(MappedRole);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string Id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ExistRole = await roleManager.RoleExistsAsync(model.Name);
                if (!ExistRole)
                {
                    var Role = await roleManager.FindByIdAsync(Id);

                    Role.Name = model.Name;

                    await roleManager.UpdateAsync(Role);

                    return RedirectToAction(nameof (Index));
                }
                else
                {
                    ModelState.AddModelError("Error", "Role is Exist !!");
                    return View("index", await roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
