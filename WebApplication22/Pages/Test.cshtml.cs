using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication22.Models;

namespace WebApplication22.Pages
{
    public class TestModel : PageModel
    {        

        public readonly Database db;

        public TestModel(Database db)
        {
            this.db = db;
        }

        public IEnumerable<People> Peoples { get; private set; }

        public void OnGet()
        {
            Peoples = db.People;
        }

        public void OnGetActive()
        {
            Peoples = db.People.Where(x => x.Active);
        }

        public void OnGetInactive()
        {
            Peoples = db.People.Where(x => x.Active == false);
        }

    }
}