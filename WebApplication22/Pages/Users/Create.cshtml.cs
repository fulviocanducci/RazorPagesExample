using System;
using System.Collections.Generic;
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

        public CreateModel(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationUser.Email = ApplicationUser.UserName;
            await UserManager.CreateAsync(ApplicationUser, Password);            

            return RedirectToPage("./Index");
        }
    }
}