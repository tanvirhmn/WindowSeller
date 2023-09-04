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
    public class IndexModel : PageModel
    {
        private readonly BlazorApp1.Server.Models.IntusPrefContext _context;

        public IndexModel(BlazorApp1.Server.Models.IntusPrefContext context)
        {
            _context = context;
        }

        public IList<FilterViewMaster> FilterViewMaster { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FilterViewMasters != null)
            {
                FilterViewMaster = await _context.FilterViewMasters.ToListAsync();
            }
        }
    }
}
