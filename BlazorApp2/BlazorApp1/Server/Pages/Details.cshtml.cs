using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Server.Models;

namespace BlazorApp1.Server.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly BlazorApp1.Server.Models.IntusPrefContext _context;

        public DetailsModel(BlazorApp1.Server.Models.IntusPrefContext context)
        {
            _context = context;
        }

      public FilterViewMaster FilterViewMaster { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FilterViewMasters == null)
            {
                return NotFound();
            }

            var filterviewmaster = await _context.FilterViewMasters.FirstOrDefaultAsync(m => m.Id == id);
            if (filterviewmaster == null)
            {
                return NotFound();
            }
            else 
            {
                FilterViewMaster = filterviewmaster;
            }
            return Page();
        }
    }
}
