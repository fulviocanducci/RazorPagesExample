using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.c
{
    public class EditModel : PageModel
    {
        private readonly WebApplication22.Models.Database _context;

        public EditModel(WebApplication22.Models.Database context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Credit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditExists(Credit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CreditExists(int id)
        {
            return _context.Credit.Any(e => e.Id == id);
        }
    }
}
