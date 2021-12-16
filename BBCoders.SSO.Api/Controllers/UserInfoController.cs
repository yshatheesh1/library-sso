// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using System.Security.Cryptography.X509Certificates;
// using System.Security.Claims;

// namespace BBCoders.SSO.Api.Controllers
// {
//     [ApiController]
//     [Route("v1/[controller]")]
//     public class UserInfoController : ControllerBase
//     {
//         private readonly ILogger<UserInfoController> _logger;

//         public UserInfoController(ILogger<UserInfoController> logger)
//         {
//             _logger = logger;
//         }

//         [HttpGet]
//         public async IActionResult GetUserInfo()
//         {
//             var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
//             if (!string.IsNullOrWhiteSpace(authorizationHeader))
//             {
//                 var header = authorizationHeader.Trim();
//                 if (header.StartsWith("Bearer"))
//                 {
//                     var value = header.Substring("Bearer".Length).Trim();
//                     if (!string.IsNullOrWhiteSpace(value))
//                     {

//                     }
//                 }
//             }
//             else if (MediaTypeHeaderValue.TryParse(Request.ContentType, out var header) && header.MediaType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
//             {
//                 var token = (await Request.ReadFormAsync())["access_token"].FirstOrDefault();
//             }
//             else
//             {
//                 _logger.LogError("No access token found.");
//                 return BadRequest();
//             }

//             return false;
//         }

//         private void ValidateToken()
//         {
//             var handler = new JwtSecurityTokenHandler();
//             handler.InboundClaimTypeMap.Clear();
//             var baseUrl = Request.Scheme + "://" + Request.Host.Value;
//             var t = new SecurityKey();
//             var parameters = new TokenValidationParameters
//             {
//                 ValidIssuer = baseUrl,
//                 IssuerSigningKeys = validationKeys.Select(k => k.Key),
//                 ValidateLifetime = true
//             };
//         }

//         private void AddSigningCredentials()
//         {
//             var rsaCert = new X509Certificate2("./keys/identityserver.test.rsa.p12", "changeit");
//             var ecCert = new X509Certificate2("./keys/identityserver.test.ecdsa.p12", "changeit");
//             var key = new ECDsaSecurityKey(ecCert.GetECDsaPrivateKey())
//             {
//                 KeyId = Guid.NewGuid().ToString()
//             };
//                        builder.AddSigningCredential(rsaCert, "RS256");

//             // ...and PS256
//             builder.AddSigningCredential(rsaCert, "PS256");
//               return builder.AddSigningCredential(
//                 key,
//                 IdentityServerConstants.ECDsaSigningAlgorithm.ES256);

//         }
//     }
// }
