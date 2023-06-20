using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockModule.DAL.Services;
using StockModule.BLL;
using StockModule.BLL.StockSettings;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockModule.API.Controllers
{
    [Route("api/StockSettings")]
    [ApiController]
    public class StockSettingsController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IStockSettingsLogic _stockSettingsLogic;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockSettingsLogic"></param>
        /// <param name="logger"></param>
        public StockSettingsController(IStockSettingsLogic stockSettingsLogic, ILogger<StockSettingsController> logger)
        {
            _stockSettingsLogic = stockSettingsLogic;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStockSettings")]
        public IActionResult GetStockSettings()
        {
            var result = _stockSettingsLogic.GetStockSettings();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStockPermissions")]
        public IActionResult GetStockPermissions()
        {
            var result = _stockSettingsLogic.GetUserPermissions();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("SavePermission")]
        public IActionResult SaveStockPermission(MaterialDto material)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                var response = _stockSettingsLogic.SaveStockSetting(material);

                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 100)
                {
                    _logger.LogInformation("Elapsed milliseconds: {elapsed}", stopwatch.ElapsedMilliseconds.ToString());
                }

                if(!response)
                {
                    throw new Exception(JsonConvert.SerializeObject(material).ToString());
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveStockPermission error");
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("updatestocksettinghierarchy")]
        public IActionResult UpdateStockSettingHierarchy([FromBody]List<int> ids, int folderHierarchyId)
        {
            try
            {

                _stockSettingsLogic.UpdateStockSetting(ids, folderHierarchyId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveStockPermission error");
                return BadRequest(ex.Message);

            }
        }
    }
}
