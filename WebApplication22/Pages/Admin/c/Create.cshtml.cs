using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication22.Models;

namespace WebApplication22.Pages.Admin.c
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication22.Models.Database _context;

        public CreateModel(WebApplication22.Models.Database context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Credit Credit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Credit.Add(Credit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}