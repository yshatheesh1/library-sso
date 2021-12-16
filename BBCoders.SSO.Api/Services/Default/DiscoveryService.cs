// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using IdentityModel;
// using Microsoft.Extensions.Logging;

// namespace BBCoders.SSO.Api.Services
// {
//     public class DiscoveryService : IDiscoveryService
//     {
//         private const string ConnectPathPrefix = "connect";
//         private const string Authorize = ConnectPathPrefix + "/authorize";
//         private const string AuthorizeCallback = Authorize + "/callback";
//         private const string DiscoveryConfiguration = ".well-known/openid-configuration";
//         private const string DiscoveryWebKeys = DiscoveryConfiguration + "/jwks";
//         private const string Token = ConnectPathPrefix + "/token";
//         private const string Revocation = ConnectPathPrefix + "/revocation";
//         private const string UserInfo = ConnectPathPrefix + "/userinfo";
//         private const string Introspection = ConnectPathPrefix + "/introspect";
//         private const string EndSession = ConnectPathPrefix + "/endsession";
//         private const string EndSessionCallback = EndSession + "/callback";
//         private const string CheckSession = ConnectPathPrefix + "/checksession";
//         private const string DeviceAuthorization = ConnectPathPrefix + "/deviceauthorization";

//         private const string MtlsPathPrefix = ConnectPathPrefix + "/mtls";
//         private const string MtlsToken = MtlsPathPrefix + "/token";
//         private const string MtlsRevocation = MtlsPathPrefix + "/revocation";
//         private const string MtlsIntrospection = MtlsPathPrefix + "/introspect";
//         private const string MtlsDeviceAuthorization = MtlsPathPrefix + "/deviceauthorization";
//         private readonly ILogger<DiscoveryService> _logger;
//         private readonly IKeyMaterialService _keyMaterialService;

//         public DiscoveryService(ILogger<DiscoveryService> logger, IKeyMaterialService keyMaterialService)
//         {
//             this._logger = logger;
//             this._keyMaterialService = keyMaterialService;
//         }
//         public Task<Dictionary<string, object>> CreateDiscoveryDocumentAsync(string baseUrl)
//         {
//             var entries = new Dictionary<string, object>
//             {
//                 { OidcConstants.Discovery.Issuer, baseUrl }
//             };

//             if (_keyMaterialService.GetValidationKeys().Any())
//             {
//                 entries.Add(OidcConstants.Discovery.JwksUri, baseUrl + DiscoveryWebKeys);
//             }

//             entries.Add(OidcConstants.Discovery.AuthorizationEndpoint, baseUrl + Authorize);


//             entries.Add(OidcConstants.Discovery.TokenEndpoint, baseUrl + Token);


//             entries.Add(OidcConstants.Discovery.UserInfoEndpoint, baseUrl + UserInfo);


//             entries.Add(OidcConstants.Discovery.EndSessionEndpoint, baseUrl + EndSession);


//             entries.Add(OidcConstants.Discovery.CheckSessionIframe, baseUrl + CheckSession);


//             entries.Add(OidcConstants.Discovery.RevocationEndpoint, baseUrl + Revocation);


//             entries.Add(OidcConstants.Discovery.IntrospectionEndpoint, baseUrl + Introspection);


//             entries.Add(OidcConstants.Discovery.DeviceAuthorizationEndpoint, baseUrl + DeviceAuthorization);

 
 

//             // scopes and claims
//             if (Options.Discovery.ShowIdentityScopes ||
//                 Options.Discovery.ShowApiScopes ||
//                 Options.Discovery.ShowClaims)
//             {
//                 var resources = await ResourceStore.GetAllEnabledResourcesAsync();
//     var scopes = new List<string>();

//                 // scopes
//                 if (Options.Discovery.ShowIdentityScopes)
//                 {
//                     scopes.AddRange(resources.IdentityResources.Where(x => x.ShowInDiscoveryDocument).Select(x => x.Name));
//                 }

// if (Options.Discovery.ShowApiScopes)
// {
//     var apiScopes = from scope in resources.ApiScopes
//                     where scope.ShowInDiscoveryDocument
//                     select scope.Name;

//     scopes.AddRange(apiScopes);
//     scopes.Add(IdentityServerConstants.StandardScopes.OfflineAccess);
// }

// if (scopes.Any())
// {
//     entries.Add(OidcConstants.Discovery.ScopesSupported, scopes.ToArray());
// }

// // claims
// if (Options.Discovery.ShowClaims)
// {
//     var claims = new List<string>();

//     // add non-hidden identity scopes related claims
//     claims.AddRange(resources.IdentityResources.Where(x => x.ShowInDiscoveryDocument).SelectMany(x => x.UserClaims));
//     claims.AddRange(resources.ApiResources.Where(x => x.ShowInDiscoveryDocument).SelectMany(x => x.UserClaims));
//     claims.AddRange(resources.ApiScopes.Where(x => x.ShowInDiscoveryDocument).SelectMany(x => x.UserClaims));

//     entries.Add(OidcConstants.Discovery.ClaimsSupported, claims.Distinct().ToArray());
// }
//             }

//             // grant types
//             if (Options.Discovery.ShowGrantTypes)
// {
//     var standardGrantTypes = new List<string>
//                 {
//                     OidcConstants.GrantTypes.AuthorizationCode,
//                     OidcConstants.GrantTypes.ClientCredentials,
//                     OidcConstants.GrantTypes.RefreshToken,
//                     OidcConstants.GrantTypes.Implicit
//                 };

//     if (!(ResourceOwnerValidator is NotSupportedResourceOwnerPasswordValidator))
//     {
//         standardGrantTypes.Add(OidcConstants.GrantTypes.Password);
//     }

//     if (Options.Endpoints.EnableDeviceAuthorizationEndpoint)
//     {
//         standardGrantTypes.Add(OidcConstants.GrantTypes.DeviceCode);
//     }

//     var showGrantTypes = new List<string>(standardGrantTypes);

//     if (Options.Discovery.ShowExtensionGrantTypes)
//     {
//         showGrantTypes.AddRange(ExtensionGrants.GetAvailableGrantTypes());
//     }

//     entries.Add(OidcConstants.Discovery.GrantTypesSupported, showGrantTypes.ToArray());
// }

// // response types
// if (Options.Discovery.ShowResponseTypes)
// {
//     entries.Add(OidcConstants.Discovery.ResponseTypesSupported, Constants.SupportedResponseTypes.ToArray());
// }

// // response modes
// if (Options.Discovery.ShowResponseModes)
// {
//     entries.Add(OidcConstants.Discovery.ResponseModesSupported, Constants.SupportedResponseModes.ToArray());
// }

// // misc
// if (Options.Discovery.ShowTokenEndpointAuthenticationMethods)
// {
//     var types = SecretParsers.GetAvailableAuthenticationMethods().ToList();
//     if (Options.MutualTls.Enabled)
//     {
//         types.Add(OidcConstants.EndpointAuthenticationMethods.TlsClientAuth);
//         types.Add(OidcConstants.EndpointAuthenticationMethods.SelfSignedTlsClientAuth);
//     }

//     entries.Add(OidcConstants.Discovery.TokenEndpointAuthenticationMethodsSupported, types);
// }

// var signingCredentials = await Keys.GetAllSigningCredentialsAsync();
// if (signingCredentials.Any())
// {
//     var signingAlgorithms = signingCredentials.Select(c => c.Algorithm).Distinct();
//     entries.Add(OidcConstants.Discovery.IdTokenSigningAlgorithmsSupported, signingAlgorithms);
// }

// entries.Add(OidcConstants.Discovery.SubjectTypesSupported, new[] { "public" });
// entries.Add(OidcConstants.Discovery.CodeChallengeMethodsSupported, new[] { OidcConstants.CodeChallengeMethods.Plain, OidcConstants.CodeChallengeMethods.Sha256 });

// if (Options.Endpoints.EnableAuthorizeEndpoint)
// {
//     entries.Add(OidcConstants.Discovery.RequestParameterSupported, true);

//     if (Options.Endpoints.EnableJwtRequestUri)
//     {
//         entries.Add(OidcConstants.Discovery.RequestUriParameterSupported, true);
//     }
// }

// if (Options.MutualTls.Enabled)
// {
//     entries.Add(OidcConstants.Discovery.TlsClientCertificateBoundAccessTokens, true);
// }

// // custom entries
// if (!Options.Discovery.CustomEntries.IsNullOrEmpty())
// {
//     foreach ((string key, object value) in Options.Discovery.CustomEntries)
//     {
//         if (entries.ContainsKey(key))
//         {
//             Logger.LogError("Discovery custom entry {key} cannot be added, because it already exists.", key);
//         }
//         else
//         {
//             if (value is string customValueString)
//             {
//                 if (customValueString.StartsWith("~/") && Options.Discovery.ExpandRelativePathsInCustomEntries)
//                 {
//                     entries.Add(key, baseUrl + customValueString.Substring(2));
//                     continue;
//                 }
//             }

//             entries.Add(key, value);
//         }
//     }
// }

// return entries;
//         }
//     }
// }