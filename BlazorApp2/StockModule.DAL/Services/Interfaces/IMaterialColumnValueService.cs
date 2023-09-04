using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services.Interfaces
{
    public interface IMaterialColumnValueService : IEntityService<MaterialColumnValue>
    {
        void UpdateFolderIdMaterialColumnValue(List<int> rowNos, int folderHierarchyId, string columnName);
        IEnumerable<MaterialColumnValue> GetByRowNo(int rowNo, string columnName);
    }
}
