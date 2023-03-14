using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using UserModule.API.Helpers;
using UserModule.API.Models;

namespace UserModule.API.Controllers
{

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class CurrentUserController : ControllerBase
    {
        private readonly ITokenAcquisition _tokenAcquisition;

        public CurrentUserController(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }
        // GET: api/<AzureAdUserController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var token = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "User.ReadBasic.All", "User.Read" });
            CachedUser user = await GraphHelper.GetCurrentUserDetailAsync(token);
            return Ok(user);
        }
    }
}
