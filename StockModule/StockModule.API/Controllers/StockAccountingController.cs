using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Identity.Web.Resource;
using StockModule.BLL;
using StockModule.BLL.Dto;
using StockModule.BLL.SubClasses.StockAccountingModels;
using StockModule.DAL.Services;


namespace StockModule.API.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   // [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class StockAccountingController : ControllerBase
    {

        private readonly ILogger<StockAccountingController> _logger;
        private readonly IStockAccountingLogic _stockAccountingLogic;

        public StockAccountingController(ILogger<StockAccountingController> logger, IStockAccountingLogic stockAccountingLogic)
        {
            _logger = logger;
            _stockAccountingLogic = stockAccountingLogic;
        }
        /// <summary>
        /// Get All Stock Accounting Movements
        /// </summary>
        /// <returns>List</returns>
        [HttpGet]           
        public IEnumerable<StockAccountingMovementDto> Get()
        {
            return _stockAccountingLogic.GetAll();
        }

        [HttpGet("filter/{filter}")]
        public IEnumerable<StockAccountingMovementDto> Filter(string? filter)
        {
            return _stockAccountingLogic.GetByFilter(filter);
        }

        /// <summary>
        /// Transfer Stock Accounting Movements to Accounting
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return success or not and error</returns>
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(StockAccontingTransferRequest request)
        {
            try
            {
                if (request == null || !request.StockAccountingIds.Any())
                    return BadRequest();

                var response = await _stockAccountingLogic.TransferToAccounting(request);

                if (response == null || !response.StockAccountingItemResponses.Any())
                    return NotFound();

                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return Conflict(response);
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        /// <summary>
        /// Create or update stock accounting by stock movement id
        /// </summary>
        /// <param name="stockMovementId"></param>
        /// <returns></returns>
        [HttpGet("create/{stockMovementId}")]
        public IActionResult Create(int stockMovementId)
        {
            try
            {
                if (_stockAccountingLogic.TryCreateOrUpdateByStockMovement(stockMovementId))
                {
                    return Ok();
                }
                return BadRequest();
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }   

        /// <summary>
        /// Create or update stock accounting by stock movement id
        /// </summary>
        /// <param name="stockMovementId"></param>
        /// <returns></returns>
        [HttpGet("createFrom/{date}")]
        public IActionResult CreateAll(string date)
        {
            try
            {
                DateTime dateTime = DateTime.Parse(date);

                var stockMovements = _stockAccountingLogic.GetAllStockMovementByFromDate(dateTime);

                foreach (var stockMovementId in stockMovements)
                {
                    if (!_stockAccountingLogic.TryCreateOrUpdateByStockMovement(stockMovementId))
                    {
                        _logger.LogWarning($"Not Create stock accounting movement id: {stockMovementId}");
                    }                 
                }
               
                return Ok();
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }


    }
}
