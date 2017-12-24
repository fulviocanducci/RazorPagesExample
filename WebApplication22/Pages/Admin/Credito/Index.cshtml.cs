using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.Credito
{
    public class IndexModel : PageModel
    {
        public readonly Database database;
            
        public IndexModel(Database database)
        {
            this.database = database;
        }

        public IEnumerable<Credit> Credits { get; private set; }

        public void OnGet()
        {
            Credits = database.Credit;
        }
    }
}