using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockModule.DAL.Models;
using StockModule.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.StockSettings
{
    public class StockSettingsLogic : IStockSettingsLogic
    {
        private readonly IMaterialService MaterialService;
        private readonly IStockSettingService StockSettingService;
        private readonly IUserPermissionService UserPermissionService;
        private readonly ILogger Logger;
        private readonly IMapper _mapper;

        public StockSettingsLogic(IMaterialService materialService, IUserPermissionService userPermissionService, ILogger<StockSettingsLogic> logger, IMapper mapper, IStockSettingService stockSettingService)
        {
            MaterialService = materialService;
            StockSettingService = stockSettingService;
            UserPermissionService = userPermissionService;
            Logger = logger;
            _mapper = mapper;            
        }

        public List<MaterialDto> GetStockSettings()
        {
            var stockSettings = MaterialService.SelectAllMaterialSettings();
            var allSettings = _mapper.Map<List<MaterialDto>>(stockSettings);
            return allSettings;
        }

        public List<UserPermissionDto> GetUserPermissions()
        {
            var userPermissions = UserPermissionService.GetPermissions();
            var permissions = _mapper.Map<List<UserPermissionDto>>(userPermissions);
            return permissions;
        }

        public bool SaveStockSetting(MaterialDto materialDto)
        {
            var material = _mapper.Map<Material>(materialDto);

            if (material?.StockSetting?.Id == 0)
            {
                material.StockSetting.MaterialId = material.Id;
                StockSettingService.Create(material.StockSetting);
            }
            else
            {
                material.StockSetting.MaterialId = material.Id;
                StockSettingService.Update(material.StockSetting);
            }

            return true;
        }

        public void UpdateStockSetting(List<int> ids, int folderHierarchyId)
        {
            StockSettingService.UpdateStockSetting(ids, folderHierarchyId); 
        }
    }
}
