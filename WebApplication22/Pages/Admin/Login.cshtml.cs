using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebApplication22.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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

        public async Task OnGetAsync()
        {
            //if (await RoleManager.RoleExistsAsync("Admim") == false)
            //{                
            //    await RoleManager.CreateAsync(new IdentityRole
            //    {
            //        Name = "Admin"
            //    });
            //}
            //if (await RoleManager.RoleExistsAsync("Default") == false)
            //{
            //    await RoleManager.CreateAsync(new IdentityRole
            //    {
            //        Name = "Default"
            //    });
            //}
            //if (UserManager.Users.Where(x => x.Email == "fulviocanducci@hotmail.com").Any() == false)
            //{
            //    await UserManager.CreateAsync(new ApplicationUser
            //    {
            //        Email = "fulviocanducci@hotmail.com",
            //        UserName = "fulviocanducci@hotmail.com"
            //    }, "A770301bc@");                
            //}

            //var a = await UserManager.FindByNameAsync("fulviocanducci@hotmail.com");
            //var b = await UserManager.FindByNameAsync("hugopirapo@hotmail.com");
            //await UserManager.AddToRoleAsync(a, "Admin");
            //await UserManager.AddToRoleAsync(a, "Default");
            //await UserManager.AddToRoleAsync(b, "Default");
            await Task.FromResult(0);
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