// using System.Threading.Tasks;
// using BBCoders.SSO.Core.Services;
// using BBCoders.SSO.Core.src.Models;

// namespace BBCoders.SSO.Core.src.Services
// {
//     public class TokenGeneratorImpl : TokenGenerator
//     {
//         public Task<TokenResponseDTO> ProcessAsync(TokenRequestDTO tokenRequestDTO)
//         {
//             switch (tokenRequestDTO.GrantType)
//             {
//                 case OidcConstants.GrantTypes.ClientCredentials:
//                     return await ProcessClientCredentialsRequestAsync(request);
//                 case OidcConstants.GrantTypes.Password:
//                     return await ProcessPasswordRequestAsync(request);
//                 case OidcConstants.GrantTypes.AuthorizationCode:
//                     return await ProcessAuthorizationCodeRequestAsync(request);
//                 case OidcConstants.GrantTypes.RefreshToken:
//                     return await ProcessRefreshTokenRequestAsync(request);
//                 case OidcConstants.GrantTypes.DeviceCode:
//                     return await ProcessDeviceCodeRequestAsync(request);
//                 default:
//                     return await ProcessExtensionGrantRequestAsync(request);
//             }
//         }


//         /// <summary>
//         /// Creates the access/refresh token.
//         /// </summary>
//         /// <param name="request">The request.</param>
//         /// <returns></returns>
//         /// <exception cref="System.InvalidOperationException">Client does not exist anymore.</exception>
//         protected virtual async Task<(string accessToken, string refreshToken)> CreateAccessTokenAsync(TokenRequestDTO tokenRequestDTO)
//         {
//             TokenCreationRequest tokenRequest;
//             bool createRefreshToken;

//             if (tokenRequestDTO.AuthorizationCode != null)
//             {
//                 createRefreshToken = request.AuthorizationCode.RequestedScopes.Contains(IdentityServerConstants.StandardScopes.OfflineAccess);

//                 // load the client that belongs to the authorization code
//                 Client client = null;
//                 if (request.AuthorizationCode.ClientId != null)
//                 {
//                     client = await Clients.FindEnabledClientByIdAsync(request.AuthorizationCode.ClientId);
//                 }
//                 if (client == null)
//                 {
//                     throw new InvalidOperationException("Client does not exist anymore.");
//                 }

//                 var parsedScopesResult = ScopeParser.ParseScopeValues(request.AuthorizationCode.RequestedScopes);
//                 var validatedResources = await Resources.CreateResourceValidationResult(parsedScopesResult);

//                 tokenRequest = new TokenCreationRequest
//                 {
//                     Subject = request.AuthorizationCode.Subject,
//                     Description = request.AuthorizationCode.Description,
//                     ValidatedResources = validatedResources,
//                     ValidatedRequest = request
//                 };
//             }
//             else if (request.DeviceCode != null)
//             {
//                 createRefreshToken = request.DeviceCode.AuthorizedScopes.Contains(IdentityServerConstants.StandardScopes.OfflineAccess);

//                 Client client = null;
//                 if (request.DeviceCode.ClientId != null)
//                 {
//                     client = await Clients.FindEnabledClientByIdAsync(request.DeviceCode.ClientId);
//                 }
//                 if (client == null)
//                 {
//                     throw new InvalidOperationException("Client does not exist anymore.");
//                 }

//                 var parsedScopesResult = ScopeParser.ParseScopeValues(request.DeviceCode.AuthorizedScopes);
//                 var validatedResources = await Resources.CreateResourceValidationResult(parsedScopesResult);

//                 tokenRequest = new TokenCreationRequest
//                 {
//                     Subject = request.DeviceCode.Subject,
//                     Description = request.DeviceCode.Description,
//                     ValidatedResources = validatedResources,
//                     ValidatedRequest = request
//                 };
//             }
//             else
//             {
//                 createRefreshToken = request.ValidatedResources.Resources.OfflineAccess;

//                 tokenRequest = new TokenCreationRequest
//                 {
//                     Subject = request.Subject,
//                     ValidatedResources = request.ValidatedResources,
//                     ValidatedRequest = request
//                 };
//             }

//             var at = await TokenService.CreateAccessTokenAsync(tokenRequest);
//             var accessToken = await TokenService.CreateSecurityTokenAsync(at);

//             if (createRefreshToken)
//             {
//                 var refreshToken = await RefreshTokenService.CreateRefreshTokenAsync(tokenRequest.Subject, at, request.Client);
//                 return (accessToken, refreshToken);
//             }

//             return (accessToken, null);
//         }

//     }
// }