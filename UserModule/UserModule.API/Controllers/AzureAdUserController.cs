using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using UserModule.API.Helpers;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserModule.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class AzureAdUserController : ControllerBase
    {
        private readonly ITokenAcquisition _tokenAcquisition;

        public AzureAdUserController(ITokenAcquisition tokenAcquisition)
        {
            _tokenAcquisition = tokenAcquisition;
        }
        // GET: api/<AzureAdUserController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var token = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "User.ReadBasic.All", "User.Read" });
            IGraphServiceUsersCollectionPage userList = GraphHelper.GetAllAzureADUsers(token);
            return Ok(userList);
        }


    }
}
