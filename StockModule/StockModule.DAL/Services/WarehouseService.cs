using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class WarehouseService : EntityService<Warehouse>, IWarehouseService
    {
        public WarehouseService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<Warehouse>();
        }

        public int GetId(string warehouse)
        {
            return Set.Where(w => w.Name == warehouse.ToString()).Select(s => s.Id).FirstOrDefault();
        }
    }
}
