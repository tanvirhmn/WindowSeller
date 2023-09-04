using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class MaterialColumnValueService : EntityService<MaterialColumnValue>, IMaterialColumnValueService
    {
        private readonly IntusPrefContext _dbContext;
        public MaterialColumnValueService(IntusPrefContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MaterialColumnValue> GetByRowNo(int rowNo, string columnName)
        {
            var dmc = _dbContext.MaterialColumn.Where(dmc => dmc.Name == columnName).FirstOrDefault();
            return _dbContext.MaterialColumnValue.Where(mcv => mcv.RowNo == rowNo && mcv.MaterialColumnId == dmc!.Id).AsEnumerable();
        }

        public void UpdateFolderIdMaterialColumnValue(List<int> rowNos, int folderHierarchyId, string columnName)
        {
            var dmc = _dbContext.MaterialColumn.Where(dmc => dmc.Name == columnName).FirstOrDefault();
            var smcvToUpdate = _dbContext.MaterialColumnValue.Where(mcv => rowNos.Contains(mcv.RowNo) && mcv.MaterialColumnId == dmc!.Id).ToList();
            smcvToUpdate.ForEach(x => x.Value = folderHierarchyId.ToString());

            _dbContext.SaveChanges();
        }
    }
}
