using Microsoft.EntityFrameworkCore;
using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class StockSettingService : EntityService<StockSetting>, IStockSettingService
    {
        private readonly IntusPrefContext _dbContext;
        public StockSettingService(IntusPrefContext context) : base(context)
        {
            _dbContext = context;
        }

        public void UpdateStockSetting(List<int> ids, int folderHierarchyId)
        {
            var stockSettingToUpdate = _dbContext.StockSettings.Where(ss => ids.Contains(ss.MaterialId)).ToList();
            stockSettingToUpdate.ForEach(x => x.FolderHierarchyId = folderHierarchyId);

            _dbContext.SaveChanges();
        }
    }
}
