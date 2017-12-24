using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.c
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication22.Models.Database _context;

        public IndexModel(WebApplication22.Models.Database context)
        {
            _context = context;
        }

        public IList<Credit> Credit { get;set; }

        public async Task OnGetAsync()
        {
            Credit = await _context.Credit.ToListAsync();
        }
    }
}
