using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebApplication22.Models;
using System.Linq;
namespace WebApplication22.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        

        public LoginModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            if (UserManager.Users.Where(x => x.Email == "fulviocanducci@hotmail.com").Any() == false)
            {
                await UserManager.CreateAsync(new ApplicationUser
                {
                    Email = "fulviocanducci@hotmail.com",
                    UserName = "fulviocanducci@hotmail.com"
                }, "A770301bc@");
            }
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

        public async Task OnGetLogoutAsync()
        {         
            await SignInManager.SignOutAsync();
            RedirectToPage("/admin/login");
        }
    }
}