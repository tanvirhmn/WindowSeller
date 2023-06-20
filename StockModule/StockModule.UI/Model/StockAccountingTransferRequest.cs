namespace StockModule.UI.Model
{
    public class StockAccountingTransferRequest
    {
        public IList<int> StockAccountingIds { get; set; } = new List<int>();
        public bool IsConfirmTransfer { get; set; } = false;

    }
}
