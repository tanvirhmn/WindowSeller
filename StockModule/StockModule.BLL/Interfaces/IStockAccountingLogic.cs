using StockModule.BLL.Dto;
using StockModule.BLL.SubClasses.StockAccountingModels;

namespace StockModule.BLL
{
    public interface IStockAccountingLogic
    {
        Task<StockAccontingTransferResponse> TransferToAccounting(StockAccontingTransferRequest accontingTransferRequest);
        bool TryCreateOrUpdateByStockMovement(int stockMovementId);
        List<StockAccountingMovementDto> GetAll();
        List<StockAccountingMovementDto> GetByFilter(string? filter);
        List<int> GetAllStockMovementByFromDate(DateTime datetime);
    }
}
