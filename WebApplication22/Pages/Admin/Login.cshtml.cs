using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebApplication22.Models;
namespace WebApplication22.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public LoginModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public void OnGet()
        {
            
        }

        [BindProperty()]
        public LoginViewModel LoginViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(LoginViewModel.Email, 
                LoginViewModel.Password, 
                LoginViewModel.Remember, 
                false);

            if (!result.Succeeded)
            {
                ModelState.Clear();
                ModelState.AddModelError(nameof(LoginViewModel.Email), "E-mail inexistente");
                ModelState.AddModelError(nameof(LoginViewModel.Password), "Senha inexistente");
                return Page();
            }

            return RedirectToPage("/admin/index");
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {         
            await SignInManager.SignOutAsync();
            return RedirectToPage("/admin/login");
        }
    }
}