using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services
{
    public class DynamicMaterialCoulmnGridHidingService : EntityService<DynamicMaterialCoulmnGridHiding>, IDynamicMaterialCoulmnGridHidingService
    {
        private readonly IntusPrefContext _dbContext;
        public DynamicMaterialCoulmnGridHidingService(IntusPrefContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
