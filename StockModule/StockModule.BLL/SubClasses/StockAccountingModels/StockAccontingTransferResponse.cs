using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.SubClasses.StockAccountingModels
{
    public class StockAccontingTransferResponse
    {
        public bool IsSuccess
        {
            get
            {
                return !StockAccountingItemResponses.Any(_ => !_.IsSuccess);
            }
        }
        public string Message
        {
            get { return string.Format("Done: {0}/{1}", StockAccountingItemResponses.Count(_ => _.IsSuccess), StockAccountingItemResponses.Count()); }
        }

        public List<StockAccontingItemResponse> StockAccountingItemResponses { get; set; } = new List<StockAccontingItemResponse>();

    }

    public class StockAccontingItemResponse
    {
        public int StockAccountingId { get; set; }
        public bool IsSuccess
        {
            get
            {
                return IsSuccessTransfer && IsSuccessTransfer1 && IsSuccessTransfer2;
            }
        }
        public bool IsSuccessTransfer { get; set; } = false;
        public bool IsSuccessTransfer1 { get; set; } = false;
        public bool IsSuccessTransfer2 { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
