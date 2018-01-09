using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace WebApplication22.Pages
{
    public class ElementosModel: PageModel
    {
        public string Message { get; private set; }
        public void OnGet()
        {
            Message = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");            
        }
    }
}
