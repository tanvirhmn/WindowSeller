using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public interface IStockService : IEntityService<Stock>
    {

        public Stock? GetByFilter(int materialId, double length, double height, int? warehouseId);
        int? GetId(int materialId, double length, double height, int? warehouseId);

        bool StockExists(int materialId, double length, double height, int? warehouseId);

   
    }
}
