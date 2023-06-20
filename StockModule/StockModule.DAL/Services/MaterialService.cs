using StockModule.DAL.Models;
using StockModule.DAL.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StockModule.DAL.DBContexts;

namespace StockModule.DAL.Services
{
    public class MaterialService : EntityService<Material>, IMaterialService
    {
        public MaterialService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<Material>();
        }
   
        public List<Material> SelectAllMaterialSettings()
        {
            return Set.Include(m => m.StockSetting).ToList();
        }

        public int GetId(string name)
        {
            return Set.Where(w => w.Code == name).Select(s => s.Id).FirstOrDefault();
        }

        public Material? GetMaterialWithStockInfo(string code, List<int> warehouses)
        {
            return Set.Include(_=>_.Stocks!.Where(__=>__.Quantity != 0.0 && warehouses.Any(___ => ___ == __.WarehouseID))).ThenInclude(_ => _.Warehouse).FirstOrDefault(_ => _.Code == code);                     
        }


        //Impelemnt jai kazko reikes. Bet update ir panasiai palieka EntityServise
    }
}
