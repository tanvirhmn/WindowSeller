using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockModule.BLL.Dto;
using StockModule.BLL.Interfaces;
using StockModule.DAL.Services;
using StockModule.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL.Logic
{
    public class View_StockSettingsMaterial_FolderHierarchyLogic : IView_StockSettingsMaterial_FolderHierarchyLogic
    {
        private readonly IView_StockSettingsMaterial_FolderHierarchyService _view_StockSettingsMaterial_FolderHierarchyService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public View_StockSettingsMaterial_FolderHierarchyLogic(
                            IView_StockSettingsMaterial_FolderHierarchyService view_StockSettingsMaterial_FolderHierarchyService,
                            ILogger<View_StockSettingsMaterial_FolderHierarchyLogic> logger,
                            IMapper mapper,
                            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _view_StockSettingsMaterial_FolderHierarchyService = view_StockSettingsMaterial_FolderHierarchyService;
        }

        public List<StockSettingsMaterial_FolderHierarchyDto> GetAll()
        {
            var result = new List<StockSettingsMaterial_FolderHierarchyDto>();
            try
            {
                var stocktockSettingsMaterial_FolderHierarchies = _view_StockSettingsMaterial_FolderHierarchyService.GetAll();
                result = _mapper.Map<List<StockSettingsMaterial_FolderHierarchyDto>>(stocktockSettingsMaterial_FolderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll error");

            }
            return result;
        }

        public List<StockSettingsMaterial_FolderHierarchyDto> GetByFilter(string? filter)
        {
            var result = new List<StockSettingsMaterial_FolderHierarchyDto>();
            try
            {
                var stocktockSettingsMaterial_FolderHierarchies = _view_StockSettingsMaterial_FolderHierarchyService.GetByFilter(filter);
                result = _mapper.Map<List<StockSettingsMaterial_FolderHierarchyDto>>(stocktockSettingsMaterial_FolderHierarchies);
                //if (!string.IsNullOrEmpty(filter) && filter != "_")
                //{
                //    stocktockSettingsMaterial_FolderHierarchies = stockAccountingMovements.Where(filter);
                //}          

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByFilter error");

            }
            return result;
        }

        public StockSettingsMaterial_FolderHierarchyDto? GetByMaterialsId(int id)
        {
            var result = new StockSettingsMaterial_FolderHierarchyDto();
            try
            {
                var stocktockSettingsMaterial_FolderHierarchies = _view_StockSettingsMaterial_FolderHierarchyService.GetByMaterialsId(id);
                result = _mapper.Map<StockSettingsMaterial_FolderHierarchyDto>(stocktockSettingsMaterial_FolderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByMaterialsId error");

            }
            return result;
        }

        public List<StockSettingsMaterial_FolderHierarchyDto> GetCustomVirtualization(int skip, int take, string? filter, string? order)
        {
            var result = new List<StockSettingsMaterial_FolderHierarchyDto>();
            try
            {
                var stocktockSettingsMaterial_FolderHierarchies = _view_StockSettingsMaterial_FolderHierarchyService.GetCustomVirtualization(skip,take,filter,order);
                result = _mapper.Map<List<StockSettingsMaterial_FolderHierarchyDto>>(stocktockSettingsMaterial_FolderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll error");

            }
            return result;
        }

        public int GetCustomVirtualizationCount(string? filter)
        {
            int result = 0;
            try
            {
                result = _view_StockSettingsMaterial_FolderHierarchyService.GetCustomVirtualizationCount(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll error");

            }
            return result;
        }

        public List<StockSettingsMaterial_FolderHierarchyDto> GetTopFive()
        {
            var result = new List<StockSettingsMaterial_FolderHierarchyDto>();
            try
            {
                var stocktockSettingsMaterial_FolderHierarchies = _view_StockSettingsMaterial_FolderHierarchyService.GetTopFive();
                result = _mapper.Map<List<StockSettingsMaterial_FolderHierarchyDto>>(stocktockSettingsMaterial_FolderHierarchies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetTopFive error");

            }
            return result;
        }
    }
}
