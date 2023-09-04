using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Server.Models;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterViewMastersController : ControllerBase
    {
        private readonly IntusPrefContext _context;

        public FilterViewMastersController(IntusPrefContext context)
        {
            _context = context;
        }

        // GET: api/FilterViewMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilterViewMaster>>> GetFilterViewMasters()
        {
          if (_context.FilterViewMasters == null)
          {
              return NotFound();
          }
            return await _context.FilterViewMasters.ToListAsync();
        }

        // GET: api/FilterViewMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilterViewMaster>> GetFilterViewMaster(int id)
        {
          if (_context.FilterViewMasters == null)
          {
              return NotFound();
          }
            var filterViewMaster = await _context.FilterViewMasters.FindAsync(id);

            if (filterViewMaster == null)
            {
                return NotFound();
            }

            return filterViewMaster;
        }

        // PUT: api/FilterViewMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilterViewMaster(int id, FilterViewMaster filterViewMaster)
        {
            if (id != filterViewMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(filterViewMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilterViewMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FilterViewMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FilterViewMaster>> PostFilterViewMaster(FilterViewMaster filterViewMaster)
        {
          if (_context.FilterViewMasters == null)
          {
              return Problem("Entity set 'IntusPrefContext.FilterViewMasters'  is null.");
          }
            _context.FilterViewMasters.Add(filterViewMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilterViewMaster", new { id = filterViewMaster.Id }, filterViewMaster);
        }

        // DELETE: api/FilterViewMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilterViewMaster(int id)
        {
            if (_context.FilterViewMasters == null)
            {
                return NotFound();
            }
            var filterViewMaster = await _context.FilterViewMasters.FindAsync(id);
            if (filterViewMaster == null)
            {
                return NotFound();
            }

            _context.FilterViewMasters.Remove(filterViewMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilterViewMasterExists(int id)
        {
            return (_context.FilterViewMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
