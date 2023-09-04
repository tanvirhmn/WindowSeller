using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services.Interfaces
{
    public interface IFilterViewMasterService : IEntityService<FilterViewMaster>
    {
        IEnumerable<FilterViewMaster> GetByAzureUserId(string azureUserId);
    }
}
