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
    public class FilterViewMasterService : EntityService<FilterViewMaster>, IFilterViewMasterService
    {
        private readonly IntusPrefContext _dbContext;
        public FilterViewMasterService(IntusPrefContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        IEnumerable<FilterViewMaster> IFilterViewMasterService.GetByAzureUserId(string azureUserId)
        {
            return _dbContext.FilterViewMaster.Where(fvm => fvm.azureUserID == azureUserId);

        }
    }
}
