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
    public class MaterialColumnService : EntityService<MaterialColumn>, IMaterialColumnService
    {
        private readonly IntusPrefContext _dbContext;
        public MaterialColumnService(IntusPrefContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
