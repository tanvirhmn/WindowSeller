using StockModule.BLL.Logic;
using StockModule.DAL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.StockSettings
{
    public interface IStockSettingsLogic
    {
        public List<MaterialDto> GetStockSettings();
        public List<UserPermissionDto> GetUserPermissions();
        public bool SaveStockSetting(MaterialDto material);
        public void UpdateStockSetting(List<int> ids, int folderHierarchyId);
    }
}
