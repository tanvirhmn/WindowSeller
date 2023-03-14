using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Security;
using UserModule.BLL.Interfaces;

namespace UserModule.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionLogic _permissionLogic;

        private readonly ILogger _logger;

        public PermissionController(IPermissionLogic permissionLogic, ILogger logger)
        {
            _permissionLogic = permissionLogic;
            _logger = logger;
        }

        [HttpGet("{azuregroups}")]
        public IActionResult Get(string azureGroups)
        {
            var result = _permissionLogic.GetByAzureGroupsAsync(azureGroups);
            if (result == null)
            {
                return NotFound(azureGroups);
            }
            return Ok(result);
        }
    }
}
