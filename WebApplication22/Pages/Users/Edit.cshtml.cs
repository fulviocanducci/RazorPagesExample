using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication22.Models;

namespace WebApplication22.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public EditModel(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Role { get; set; }

        private void SelectListRoles(string roleId)
        {
            var roles = RoleManager.Roles.Select(x => new { x.Id, x.Name });
            ViewData["Role"] = new SelectList(roles, "Id", "Name", roleId);
        }

        public async Task<IActionResult> OnGet(string id)
        {
            ApplicationUser = await UserManager.FindByIdAsync(id);
            IList<string> roles = await UserManager.GetRolesAsync(ApplicationUser);
            if (roles.Count > 0)
            {
                IdentityRole identityRole = await RoleManager.FindByNameAsync(roles[0]);
                SelectListRoles(identityRole.Id);
            }
            else
            {
                SelectListRoles(null);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SelectListRoles(Role);
                return Page();
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(ApplicationUser.Id);
            if (!string.IsNullOrEmpty(Password))
            {
                await UserManager.RemovePasswordAsync(applicationUser);
                await UserManager.AddPasswordAsync(applicationUser, Password);
            }
            IList<string> rolesUser = await UserManager.GetRolesAsync(ApplicationUser);
            string roleName = (await RoleManager.FindByIdAsync(Role)).Name;
            if (rolesUser[0] != roleName)
            {
                await UserManager.RemoveFromRolesAsync(applicationUser, rolesUser);
                await UserManager.AddToRoleAsync(applicationUser, roleName);
            }
            return RedirectToPage("./Index");
        }
    }
}