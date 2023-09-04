using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Server.Models;

namespace BlazorApp1.Server.Pages
{
    public class EditModel : PageModel
    {
        private readonly BlazorApp1.Server.Models.IntusPrefContext _context;

        public EditModel(BlazorApp1.Server.Models.IntusPrefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FilterViewMaster FilterViewMaster { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FilterViewMasters == null)
            {
                return NotFound();
            }

            var filterviewmaster =  await _context.FilterViewMasters.FirstOrDefaultAsync(m => m.Id == id);
            if (filterviewmaster == null)
            {
                return NotFound();
            }
            FilterViewMaster = filterviewmaster;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FilterViewMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilterViewMasterExists(FilterViewMaster.Id))
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

        private bool FilterViewMasterExists(int id)
        {
          return (_context.FilterViewMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
