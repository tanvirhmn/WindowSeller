using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;

namespace StockModule.DAL.Services
{
    public class StockMovementReasonService : EntityService<StockMovementReason>, IStockMovementReasonService
    {
        public StockMovementReasonService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<StockMovementReason>();
        }

        public int GetId(string reason)
        {
            return Set.Where(w => w.Name == reason).Select(s => s.Id).FirstOrDefault();
        }

        public StockMovementReason? GetByReason(string reason)
        {
            return Set.FirstOrDefault(w => w.Name == reason);
        }
    }
}
