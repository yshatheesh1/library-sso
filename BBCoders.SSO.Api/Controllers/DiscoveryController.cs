using BBCoders.SSO.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BBCoders.SSO.Api.Controllers
{
    [ApiController]
    [Route("connect/[controller]")]
    public class DiscoveryController : ControllerBase
    {
        private readonly ILogger<DiscoveryController> _logger;

        /*
public const string Authorize = "Authorize";
public const string Token = "Token";
public const string DeviceAuthorization = "DeviceAuthorization";
public const string Discovery = "Discovery";
public const string Introspection = "Introspection";
public const string Revocation = "Revocation";
public const string EndSession = "Endsession";
public const string CheckSession = "Checksession";
public const string UserInfo = "Userinfo";
*/

        public DiscoveryController(ILogger<DiscoveryController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult GetDocument()
        {
            return null;
        }

    }
}