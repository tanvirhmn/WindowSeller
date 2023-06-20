using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockModule.BLL;
using StockModule.BLL.Dto;
using StockModule.BLL.Dto.FolderHierarchy;
using StockModule.BLL.Dto.FolderHierarchy.Validators;
using StockModule.BLL.Interfaces;
using System.Drawing;
using System.Net;
using WindowSellerWASM.BLL.Responses;

namespace StockModule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialSearchController : Controller
    {
        private readonly ILogger<MaterialSearchController> _logger;
        private readonly IView_StockSettingsMaterial_FolderHierarchyLogic _stockSettingsMaterial_FolderHierarchyLogic;


        #region StockSettingsMaterial_FolderHierarchyDto

        /// <summary>
        /// Constructor with DI
        /// </summary>
        /// <param name="logger"></param>
        public MaterialSearchController(ILogger<MaterialSearchController> logger, IView_StockSettingsMaterial_FolderHierarchyLogic stockSettingsMaterial_FolderHierarchyLogic)
        {
            _logger = logger;
            _stockSettingsMaterial_FolderHierarchyLogic = stockSettingsMaterial_FolderHierarchyLogic;
        }
        /// <summary>
        /// Get All Materials
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StockSettingsMaterial_FolderHierarchyDto> Get()
        {
            return _stockSettingsMaterial_FolderHierarchyLogic.GetAll();
        }

        /// <summary>
        /// Get Materials by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("{filter}")]
        public IEnumerable<StockSettingsMaterial_FolderHierarchyDto> GetByFilter(string? filter)
        {
            return _stockSettingsMaterial_FolderHierarchyLogic.GetByFilter(filter);
        }

        /// <summary>
        /// Get Top Five Material 
        /// </summary>
        /// <returns></returns>
        [HttpGet("gettopfive")]
        public IEnumerable<StockSettingsMaterial_FolderHierarchyDto> GetTopFive()
        {
            return _stockSettingsMaterial_FolderHierarchyLogic.GetTopFive();
        }

        /// <summary>
        /// Get Materials for custom virtual
        /// </summary>
        /// <returns></returns>
        [HttpGet("getcustomvirtualization")]
        public IEnumerable<StockSettingsMaterial_FolderHierarchyDto> GetCustomVirtualization(string? filter, string? order, int skip, int take)
        {
            return _stockSettingsMaterial_FolderHierarchyLogic.GetCustomVirtualization(skip, take,filter, order);
        }

        /// <summary>
        /// Get Materials count for custom virtual
        /// </summary>
        /// <returns></returns>
        [HttpGet("getcustomvirtualizationcount")]
        public int GetCustomVirtualizationCount(string? filter)
        {
            return _stockSettingsMaterial_FolderHierarchyLogic.GetCustomVirtualizationCount(filter);
        }

        #endregion

    }
}
