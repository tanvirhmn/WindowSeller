using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlazorApp1.Server.Models;

namespace BlazorApp1.Server.Pages
{
    public class CreateModel : PageModel
    {
        private readonly BlazorApp1.Server.Models.IntusPrefContext _context;

        public CreateModel(BlazorApp1.Server.Models.IntusPrefContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FilterViewMaster FilterViewMaster { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.FilterViewMasters == null || FilterViewMaster == null)
            {
                return Page();
            }

            _context.FilterViewMasters.Add(FilterViewMaster);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
