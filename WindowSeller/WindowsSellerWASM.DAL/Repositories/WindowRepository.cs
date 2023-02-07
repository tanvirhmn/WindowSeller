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
    public class WindowRepository : GenericRepository<Window>, IWindowRepository
    {
        private readonly WindowSellerDdContext _dbContext; 
        
        public WindowRepository(WindowSellerDdContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Window>> GetByOrderIdAsync(int orderId)
        {
            var windows = await _dbContext.Windows
            .Where(wndw => wndw.OrderId == orderId)
            .Include(wndw => wndw.SubElements)
            .AsNoTracking()
            .ToListAsync();

            return windows;
        }
    }
}
