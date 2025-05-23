﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Window>> GetByOrderIdAsync(long orderId)
        {
            var windows = await _dbContext.Windows
            .Where(wndw => wndw.OrderId == orderId)
            .ToListAsync();

            return windows;
        }
        public async Task UpdateTotalSubELementsAsync(int count, long windowId)
        {
            var window = await _dbContext.Windows
            .SingleAsync(wndw => wndw.WindowId == windowId);

            window.TotalSubELements = window.TotalSubELements + count;

            _dbContext.Entry(window).State = EntityState.Modified;

        }
    }
}
