using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockModule.BLL
{
    public enum StockDocuments
    {
        NoDocument,
        Task,
        Inventory,
        DeliveryNote,
        OrderNumber,
        Lot,
        Reproduction
    }
}
