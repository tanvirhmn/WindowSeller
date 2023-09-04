using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.DAL.Services.Interfaces
{
    public interface IView_StockSettingsMaterial_FolderHierarchyService
    {
        IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetAll();
        IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetTopFive();
        IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetByFilter(string? filter);
        IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetCustomVirtualization(int skip, int take, string? filter, string? order);
        int GetCustomVirtualizationCount(string? filter);
        IQueryable<View_StockSettingsMaterial_FolderHierarchy> GetAllQueryable();
        View_StockSettingsMaterial_FolderHierarchy? GetByStockSettingsId(int id);
        View_StockSettingsMaterial_FolderHierarchy? GetByMaterialsId(int id);
        IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetByFolderHierarchyId(int id);
        IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetAllNodes(string names);
        IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetAllNodes();
    }
}
