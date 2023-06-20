using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class ExternalMovementService : EntityService<ExternalMovement>, IExternalMovementService
    {
        public ExternalMovementService(IntusPrefContext context) : base(context)
        {
            Set = context.Set<ExternalMovement>();
        }
    }
}
