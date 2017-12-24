using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.Credito
{
    public class CreateModel : PageModel
    {
        public readonly Database database;

        public CreateModel(Database database)
        {
            this.database = database;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty()]
        public Credit Credit { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            database.Credit.Add(Credit);
            database.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}