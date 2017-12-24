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
    public class DetailsModel : PageModel
    {
        private readonly WebApplication22.Models.Database _context;

        public DetailsModel(WebApplication22.Models.Database context)
        {
            _context = context;
        }

        public Credit Credit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Credit = await _context.Credit.SingleOrDefaultAsync(m => m.Id == id);

            if (Credit == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
