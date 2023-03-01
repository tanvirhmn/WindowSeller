using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowSeller.Domain;
using WindowSellerWASM.Shared.Persistance;

namespace WindowsSellerWASM.DAL.Repositories
{
    public class GlassRepository : GenericRepository<Glass>, IGlassRepository
    {
        private readonly WindowSellerDdContext _dbContext;

        public GlassRepository(WindowSellerDdContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Glass>> GetAllSortedAsync()
        {
            var glasses = await _dbContext.Glasses
            .OrderBy(gls => gls.Height)
            .ToListAsync();

            return glasses;
        }
    }
}
