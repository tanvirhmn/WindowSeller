using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public interface IWarehouseService : IEntityService<Warehouse>
    {
        int GetId(string warehouse);
    }
}
