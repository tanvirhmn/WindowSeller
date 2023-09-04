using Microsoft.EntityFrameworkCore;
using StockModule.DAL.DBContexts;
using StockModule.DAL.Models;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StockModule.DAL.Services
{
    internal class View_StockSettingsMaterial_FolderHierarchyService : IView_StockSettingsMaterial_FolderHierarchyService
    {
        private readonly IntusPrefContext _dbContext;
        public View_StockSettingsMaterial_FolderHierarchyService(IntusPrefContext dbContext) 
        {
            _dbContext = dbContext; 
        }
        public  IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetAll()
        {
           return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(ssfb=>ssfb.StockSettingsId != null).AsEnumerable();
        }

        public IQueryable<View_StockSettingsMaterial_FolderHierarchy> GetAllQueryable()
        {
            return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(ssfb => ssfb.StockSettingsId != null).AsQueryable();
        }

        public IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetByFilter(string? filter)
        {
            var r = GetAllQueryable();

            if (!string.IsNullOrEmpty(filter) && filter != "_")
            {
                r = r.Where(filter).AsQueryable();
            }
            r = r.OrderBy(_ => _.Code).ThenByDescending(_ => _.Description);
            return r.ToList();
        }

        public IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetByFolderHierarchyId(int folderHierarchyId)
        {
            return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(vw => vw.FolderHierarchyId == folderHierarchyId).AsEnumerable();
        }

        public View_StockSettingsMaterial_FolderHierarchy? GetByMaterialsId(int materialsId)
        {
            return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(vw => vw.Id == materialsId).SingleOrDefault();
        }

        public View_StockSettingsMaterial_FolderHierarchy? GetByStockSettingsId(int stockSettingsId)
        {
            return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(vw => vw.StockSettingsId == stockSettingsId).SingleOrDefault();
        }

        public IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetTopFive()
        {
            return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Take(5).OrderBy(vwstock=> vwstock.Id).AsEnumerable();
        }
        public IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetAllNodes(string names)
        {
            return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(fldrHirarchy => (EF.Functions.Like(fldrHirarchy.Description, "%" + names + "%") || EF.Functions.Like(fldrHirarchy.Code, "%" + names + "%")) && fldrHirarchy.FolderHierarchyId.HasValue).AsEnumerable();
        }

        public IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetAllNodes()
        {
            return _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(fldrHirarchy => fldrHirarchy.StockSettingsId.HasValue && fldrHirarchy.FolderHierarchyId.HasValue).AsEnumerable();
        }

        public IEnumerable<View_StockSettingsMaterial_FolderHierarchy> GetCustomVirtualization(int skip, int take, string? filter, string? orderby)
        {
            var query = _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(fldrHirarchy => fldrHirarchy.StockSettingsId.HasValue).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                query = query.OrderBy(orderby);
            }

            return query.Skip(skip).Take(take).AsEnumerable();
             
        }

        public int GetCustomVirtualizationCount(string? filter)
        { 
            var query = _dbContext.View_StockSettinsMaterialView_FolderHierarchies.Where(fldrHirarchy => fldrHirarchy.StockSettingsId.HasValue).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(filter);
            }
            return query.Count();
        }
    }
}
