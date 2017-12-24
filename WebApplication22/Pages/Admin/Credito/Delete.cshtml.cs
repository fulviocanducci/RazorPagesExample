using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.Credito
{
    public class DeleteModel : PageModel
    {
        public readonly Database database;

        public DeleteModel(Database database)
        {
            this.database = database;
        }

        [BindProperty()]
        public Credit Credit { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            

            Credit = database.Credit.Find(id.Value);

            if (Credit == null)
            {
                return NotFound();
            }            

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cr = database.Credit.Find(id);

            if (cr != null)
            {
                database.Credit.Remove(cr);
                database.SaveChanges();
            }

            return RedirectToPage("./Index");
        }

    }
}