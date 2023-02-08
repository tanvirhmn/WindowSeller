using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.Shared.Persistance;

namespace WindowsSellerWASM.DAL.Repositories
{
    public class SubElementRepository : GenericRepository<SubElement>, ISubElementRepository
    {
        private readonly WindowSellerDdContext _dbContext;

        public SubElementRepository(WindowSellerDdContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<SubElement>> GetByWindowIdAsync(long windowId)
        {
            var windows = await _dbContext.SubElements
            .Where(sbel => sbel.WindowId == windowId)
            .AsNoTracking()
            .ToListAsync();

            return windows;
        }
    }
}
