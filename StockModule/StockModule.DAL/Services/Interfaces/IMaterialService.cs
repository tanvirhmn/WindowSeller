using StockModule.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public interface IMaterialService : IEntityService<Material>
    {
        List<Material> SelectAllMaterialSettings();

        int GetId(string name);

        Material? GetMaterialWithStockInfo(string code, List<int> warehouses);
    }
}
