using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication22.Models;

namespace WebApplication22.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public CreateModel(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private void SelectListRoles()
        {
            var roles = RoleManager.Roles.Select(x => new { x.Id, x.Name });
            ViewData["Role"] = new SelectList(roles, "Id", "Name");
        }

        public IActionResult OnGet()
        {
            SelectListRoles();
            return Page();
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Role { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SelectListRoles();
                return Page();
            }

            ApplicationUser.Email = ApplicationUser.UserName;
            await UserManager.CreateAsync(ApplicationUser, Password);
            await UserManager.AddToRoleAsync(ApplicationUser, (await RoleManager.FindByIdAsync(Role)).Name);
            return RedirectToPage("./Index");
        }
    }
}