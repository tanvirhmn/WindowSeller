using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class StockDocumentService : EntityService<StockDocument>, IStockDocumentService
    {
        public StockDocumentService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<StockDocument>();
        }

        public int GetID(string document)
        {
            return Set.Where(w => w.Name == document.ToString()).Select(s => s.Id).First();
        }
    }
}
