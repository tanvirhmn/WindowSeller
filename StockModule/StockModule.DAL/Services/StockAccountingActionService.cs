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
using StockModule.DAL.DBContexts;

namespace StockModule.DAL.Services
{
    public class StockAccountingActionService : EntityService<StockAccountingAction>, IStockAccountingActionService
    {
        public StockAccountingActionService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<StockAccountingAction>();
        }

        public StockAccountingAction? GetLastByStockAccountingMovement(int stockAccountingMovement, string method)
        {
            return Set.LastOrDefault(_ => _.StockAccountingMovementId == stockAccountingMovement && _.Method == method);
        }
    }
}
