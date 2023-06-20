using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockModule.BLL;
using StockModule.BLL.Dto;
using StockModule.BLL.Interfaces;
using System.Drawing;
using System.Net;

namespace StockModule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialDataController : Controller
    {
        private readonly ILogger<MaterialDataController> _logger;
        private readonly IMaterialLogic _materialLogic;

        public MaterialDataController(ILogger<MaterialDataController> logger, IMaterialLogic materialLogic)
        {
            _logger = logger;
            _materialLogic = materialLogic;
        }

        [HttpPost]
        public async Task<IActionResult> GetMaterialDescriptions([FromBody] MaterialDescriptionRequestDTO materialDescriptionDTO)
        {
            return Ok(await _materialLogic.GetMaterialsDescriptionsAsync(materialDescriptionDTO));
        }
    }
}
