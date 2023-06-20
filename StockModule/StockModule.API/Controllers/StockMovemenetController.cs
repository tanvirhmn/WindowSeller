using StockModule.BLL;
using StockModule.BLL.Logic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockModule.API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockMovemenetController : ControllerBase
    {
        private readonly IMaterialLogic _materialLogic;
        private readonly ILogger _logger;

        public StockMovemenetController(IMaterialLogic materialLogic, ILogger<StockMovemenetController> logger)
        {
            _materialLogic = materialLogic;
            _logger = logger;
        }

        // POST api/<StockMovemenetController>
        //[HttpPost("CollectionMove")]
        //public void Post([FromBody] object value, [FromQuery] string sender)
        //{
        //    try
        //    {
        //        Stopwatch stopwatch = Stopwatch.StartNew();

        //        var movement = JsonConvert.DeserializeObject<List<CollectionMovement>>(value.ToString());
        //        //dynamic jsonObj = JsonConvert.DeserializeObject(value.ToString());

        //        bool response = false;

        //        switch (sender)
        //        //switch (jsonObj.Sender)
        //        {
        //            case "Collection":
        //                response = _materialLogic.MoveStockFromCollectionApi(movement);
        //                break;
        //        }

        //        if (!response) Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        stopwatch.Stop();
        //        if (stopwatch.ElapsedMilliseconds > 100)
        //        {
        //            _logger.LogInformation("Elapsed milliseconds: {elapsed}, Movement count: {moveCount}, {user}", 
        //                stopwatch.ElapsedMilliseconds.ToString(), 
        //                movement.Count.ToString(), 
        //                Environment.UserName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        _logger.LogError(ex, "");
        //    }
        //}

        [HttpPost("TriggerExternalMovement")]
        public void Post()
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                bool response = false;
                if (!_materialLogic.ExternalMovementBusy)
                {
                    response = _materialLogic.MoveExternalStock();
                }
                else
                {
                    _logger.LogInformation("Movement busy");
                }

                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 100)
                {
                    _logger.LogInformation("Elapsed milliseconds: {elapsed}, {user}", 
                        stopwatch.ElapsedMilliseconds.ToString(), 
                        Environment.UserName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TriggerExternalMovement error");
            }
        }

        [HttpGet("{warehouseId}/{code}")]
        public IActionResult Get(Warehouses warehouseId,  string code)
        {
            var result = _materialLogic.GetMaterialStockInfo(code, warehouseId);
            if (result ==null)
            {
                return NotFound(code);
            }
            return Ok(result);          
        }

        [HttpPost("Remnant")]
        public IActionResult PostRemnant(RequestMoveStock requestMoveStock)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                //var requestMoveStock = JsonConvert.DeserializeObject<RequestMoveStock>(value.ToString());
             
                bool response = false;
                var errMesage = string.Empty;
                response = _materialLogic.MoveStocks(requestMoveStock, out errMesage);    
               
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 100)
                {
                    _logger.LogInformation("Elapsed milliseconds: {elapsed}, Movement count: {moveCount}, {user}",
                        stopwatch.ElapsedMilliseconds.ToString(),
                        requestMoveStock.MoveStocks.Count.ToString(),
                        Environment.UserName);
                }
                if (!response)
                {
                    //throw new Exception(errMesage);
                    _logger.LogError(errMesage, "MoveStocks error. Request: "+ JsonConvert.SerializeObject(requestMoveStock).ToString());
                    return NotFound(errMesage);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {              
                _logger.LogError(ex, "Request: "+ JsonConvert.SerializeObject(requestMoveStock).ToString());
                return BadRequest(ex.Message);            
            }          
        }

        [HttpPost("ReproductionMovement")]
        public IActionResult ReproductionMovement(ReproductionMovement movement)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                var response = _materialLogic.MoveReproduction(movement);

                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 100)
                {
                    _logger.LogInformation("Elapsed milliseconds: {elapsed}", stopwatch.ElapsedMilliseconds.ToString());
                }
                if (!response)
                {
                    throw new Exception(JsonConvert.SerializeObject(movement).ToString());
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ReproductionMovement error");
                return BadRequest(ex.Message);
            }
        }
    }
       
}
