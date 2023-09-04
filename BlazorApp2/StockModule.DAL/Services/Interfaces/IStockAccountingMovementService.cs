using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public interface IStockAccountingMovementService : IEntityService<StockAccountingMovement>
    {
        bool IsExistByStockMovement(int stokMovementId);

        int CreateByStockMovement(int stokMovementId, int status = 0);
        
        IEnumerable<StockAccountingMovement> GetByFilter(string? filter);
        IEnumerable<StockAccountingMovement> GetFullAll();
        StockAccountingMovement? GetFirstByReasonAndStatus(int reasonId, int[] inStatus, int? fromStockId, int? toStockId, double quantity);
        StockAccountingMovement GetFullInfoById(int id);
        IQueryable<StockAccountingMovement> GetQueryableAllWithIncludes();
    }
}
