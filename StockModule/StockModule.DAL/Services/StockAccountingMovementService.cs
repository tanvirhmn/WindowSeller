using Microsoft.EntityFrameworkCore;
using StockModule.DAL.Migrations;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata.Ecma335;
using StockModule.DAL.DBContexts;

namespace StockModule.DAL.Services
{
    public class StockAccountingMovementService : EntityService<StockAccountingMovement>, IStockAccountingMovementService
    {
        public StockAccountingMovementService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<StockAccountingMovement>();
        }


        public bool IsExistByStockMovement(int stokMovementId)
        {
            return Set.Any(_ => _.StockMovementId == stokMovementId);          
        }

        public int CreateByStockMovement(int stokMovementId, int status=0)          
        {           
              var r = new StockAccountingMovement() { StockMovementId = stokMovementId, Status = status, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
              Create(r);
              return r.Id;

        }       

        public IEnumerable<StockAccountingMovement> GetByFilter(string? filter)
        {
            var r = GetQueryableAllWithIncludes();

            if(!string.IsNullOrEmpty(filter) && filter != "_")
            {
                r = r.Where(filter).AsQueryable();
            }           
            r = r.OrderBy(_ => _.Status).ThenByDescending(_ => _.CreatedDate);
            return r.ToList();               

        }

        public IEnumerable<StockAccountingMovement> GetFullAll()
        {
            return GetQueryableAllWithIncludes().ToList();
        }

        public IQueryable<StockAccountingMovement> GetQueryableAllWithIncludes()
        {
            return Set
                .Include(_ => _.StockAccountingActions)
                .Include(_ => _.StockMovement).ThenInclude(_ => _.ToStock).ThenInclude(_ => _!.Material)
                .Include(_ => _.ChangedByStockMovement)
                .Include(_ => _.StockMovement).ThenInclude(_ => _.StockMovementReason).ThenInclude(_ => _.FromWarehouse)
                .Include(_ => _.StockMovement).ThenInclude(_ => _.StockMovementReason).ThenInclude(_ => _.ToWarehouse).AsQueryable();               

        }

        public StockAccountingMovement GetFullInfoById(int id)
        {
            return GetQueryableAllWithIncludes().First(_ => _.Id == id);
        }

        public StockAccountingMovement? GetFirstByReasonAndStatus(int reasonId, int[] inStatus, int? fromStockId, int? toStockId)
        {
           return Set.Include(_ => _.StockMovement).Where(_ => inStatus.Any(__=>__==_.Status) && _.StockMovement.StockMovementReason.Id == reasonId).OrderBy(_ => _.Status).FirstOrDefault(_ => _.StockMovement.FromStockId == fromStockId && _.StockMovement.ToStockId == toStockId);
        }

    }
}
