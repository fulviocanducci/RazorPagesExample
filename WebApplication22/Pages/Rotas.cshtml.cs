using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication22.Pages
{
    [Route("r2/{id:int?}")]
    public class RotasModel : PageModel
    {        
        public void OnGet(int? id, DateTime? d)
        {
            ViewData["id"] = id;
            ViewData["date"] = d;
            ViewData["routes"] = RouteData.Values;
        }
    }
}