using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication22.Models;

namespace WebApplication22.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> UserManager;       

        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;            
        }

        public ApplicationUser ApplicationUser { get; private set; }

        public async Task OnGet(string id)
        {
            ApplicationUser = await UserManager.FindByIdAsync(id);
        }

        public async Task<IActionResult> OnPost(string id)
        {
            var userDelete = await UserManager.FindByIdAsync(id);
            if (userDelete != null)
            {
                IList<string> roles = await UserManager.GetRolesAsync(userDelete);
                if (roles != null && roles.Count > 0)
                {
                    await UserManager.RemoveFromRolesAsync(userDelete, roles);
                }
                await UserManager.DeleteAsync(userDelete);
            }
            return RedirectToPage("./Index");
        }
    }
}