using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.Credito
{
    public class EditModel : PageModel
    {
        public readonly Database database;

        public EditModel(Database database)
        {
            this.database = database;
        }
                
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Credit = database.Credit.Find(id);

            if (Credit == null)
            {
                return NotFound();
            }

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

            database.Credit.Update(Credit);
            database.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}