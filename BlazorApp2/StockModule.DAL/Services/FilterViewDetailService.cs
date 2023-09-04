using Microsoft.EntityFrameworkCore;
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
    public class FilterViewDetailService : EntityService<FilterViewDetail>, IFilterViewDetailService
    {
        private readonly IntusPrefContext _dbContext;
        public FilterViewDetailService(IntusPrefContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FilterViewDetail> GetByFilterViewParentId(int filterViewParentId)
        {
            return _dbContext.FilterViewDetail.Where(fvd => fvd.ParentId == filterViewParentId);
        }

        IEnumerable<FilterViewDetail> IFilterViewDetailService.GetByFilterViewMasterId(int filterViewMasterId)
        {
            return _dbContext.FilterViewDetail.Where(fvd => fvd.FilterViewMasterID == filterViewMasterId);
        }
    }
}
