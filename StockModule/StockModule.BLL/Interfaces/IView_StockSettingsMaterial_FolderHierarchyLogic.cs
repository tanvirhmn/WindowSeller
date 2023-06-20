using StockModule.BLL.Dto;
using StockModule.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.Interfaces
{
    public interface IView_StockSettingsMaterial_FolderHierarchyLogic
    {
        List<StockSettingsMaterial_FolderHierarchyDto> GetAll();
        List<StockSettingsMaterial_FolderHierarchyDto> GetTopFive();
        List<StockSettingsMaterial_FolderHierarchyDto> GetByFilter(string? filter);
        List<StockSettingsMaterial_FolderHierarchyDto> GetCustomVirtualization(int skip, int take, string? filter, string? order);
        int GetCustomVirtualizationCount(string? filter);
        StockSettingsMaterial_FolderHierarchyDto? GetByMaterialsId(int id);
    }
}
