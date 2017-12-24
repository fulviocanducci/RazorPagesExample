using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.Credito
{
    public class DetailsModel : PageModel
    {
        public Credit Credit { get; set; }

        public readonly Database database; 

        public DetailsModel(Database database)
        {
            this.database = database;
        }

        public IActionResult OnGet(int? id)
        {            
            if (id == null)
            {
                return RedirectToPage("./Index");
            }

            Credit = database.Credit.Find(id.Value);

            if (Credit == null)
            {
                return RedirectToPage("./Index");
            }
            
            return Page();
        }
        
    }
}