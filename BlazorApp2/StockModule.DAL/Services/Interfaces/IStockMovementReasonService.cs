using StockModule.DAL.Models;

namespace StockModule.DAL.Services
{
    public interface IStockMovementReasonService : IEntityService<StockMovementReason>
    {
        int GetId(string reason);

        public StockMovementReason? GetByReason(string reason);
    }
}
