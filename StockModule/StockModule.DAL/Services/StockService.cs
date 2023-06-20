using StockModule.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using StockModule.DAL.DBContexts;

namespace StockModule.DAL.Services
{
    public class StockService : EntityService<Stock>, IStockService
    {
        public StockService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<Stock>();
        }

        public int? GetId(int materialId, double length, double height, int? warehouseId)
        {
            if (!warehouseId.HasValue || warehouseId == 0 ) return null;

            if (!StockExists(materialId, length, height, warehouseId.Value))
            {
                Stock stock = new()
                {
                    MaterialId = materialId,
                    WarehouseID = warehouseId.Value,
                    Length = length,
                    Height = height,
                    Quantity = 0.0
                };

                Create(stock);
            }

            return Set.Where(w => w.MaterialId == materialId && w.Length == length && w.Height == height && w.WarehouseID == warehouseId).Select(s => s.Id).FirstOrDefault();
        }

        public Stock? GetByFilter(int materialId, double length, double height, int? warehouseId)
        {
            if (!warehouseId.HasValue || warehouseId == 0) return null;
            return Set.FirstOrDefault(_ => _.MaterialId == materialId && _.Length == length && _.Height == height && _.WarehouseID == warehouseId);
        }



        public async Task<Stock?> GetAsync(int materialId, double length, double height, int warehouseId)
        {
            if (warehouseId == 0) return null;

            if (!StockExists(materialId, length, height, warehouseId))
            {
                var stock = new Stock()
                {
                    MaterialId = materialId,
                    WarehouseID = warehouseId,
                    Length = length,
                    Height = height,
                    Quantity = 0.0
                };
               Create(stock);
            }
            return await Set.FirstOrDefaultAsync(w => w.MaterialId == materialId && w.Length == length && w.Height == height && w.WarehouseID == warehouseId);
        }

        public bool StockExists(int materialId, double length, double height, int? warehouseId)
        {
            return Set.Any(w => w.MaterialId == materialId && w.Length == length && w.Height == height && w.WarehouseID == warehouseId);
        }


     

    }
}
