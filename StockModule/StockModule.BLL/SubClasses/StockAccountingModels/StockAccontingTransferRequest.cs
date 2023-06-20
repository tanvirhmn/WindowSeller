using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.SubClasses.StockAccountingModels
{
    public class StockAccontingTransferRequest
    {
        public IList<int> StockAccountingIds { get; set; } = new List<int>();

        public bool IsConfirmTransfer { get; set; } = false;
    }

}
