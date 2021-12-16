// using System;
// using System.Globalization;
// using System.Linq;
// using System.Security.Claims;
// using System.Threading;
// using System.Threading.Tasks;
// using BBCoders.SSO.Core.Models;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
// using Microsoft.IdentityModel.Protocols.OpenIdConnect;

// namespace BBCoders.SSO.Core.Services
// {
//     /// <summary>
//     /// Manages user token
//     /// </summary>
//     public class TokenServiceImpl : TokenService
//     {
//         private const string TokenPrefix = ".Token.";
//         private readonly IHttpContextAccessor _contextAccessor;
//         private readonly ISystemClock _clock;

//         private ILogger<TokenServiceImpl> _logger;

//         /// <summary>
//         /// ctor
//         /// </summary>
//         /// <param name="logger"></param>
//         /// <param name="contextAccessor"></param>
//         /// <param name="clock"></param>
//         public TokenServiceImpl(ILogger<TokenServiceImpl> logger,
//                                 IHttpContextAccessor contextAccessor,
//                                 ISystemClock clock)
//         {
//             this._logger = logger;
//             this._contextAccessor = contextAccessor;
//             this._clock = clock;
//         }

//         /// <inheritdoc />
//         public Task<string> GetUserAccessTokenAsync(ClaimsPrincipal user,
//                                                     TokenParameters parameters = null,
//                                                     CancellationToken cancellationToken = default)
//         {
//             parameters ??= new TokenParameters();

//             if (user == null || !user.Identity.IsAuthenticated)
//             {
//                 return null;
//             }

//             var userName = user.FindFirst(JwtClaimTypes.Name)?.Value ?? user.FindFirst(JwtClaimTypes.Subject)?.Value ?? "unknown";
//             var userToken = await GetTokenAsync(user, parameters);

//             if (userToken == null)
//             {
//                 _logger.LogDebug("No token data found in user token store for user {user}.", userName);
//                 return null;
//             }

//             if (!string.IsNullOrWhiteSpace(userToken.AccessToken) && string.IsNullOrWhiteSpace(userToken.RefreshToken))
//             {
//                 _logger.LogDebug("No refresh token found in user token store for user {user} / resource {resource}. Returning current access token.", userName, parameters.Resource ?? "default");
//                 return userToken.AccessToken;
//             }

//             if (string.IsNullOrWhiteSpace(userToken.AccessToken) && !string.IsNullOrWhiteSpace(userToken.RefreshToken))
//             {
//                 _logger.LogDebug(
//                     "No access token found in user token store for user {user} / resource {resource}. Trying to refresh.",
//                     userName, parameters.Resource ?? "default");
//             }

//             var dtRefresh = DateTimeOffset.MinValue;
//             if (userToken.Expiration.HasValue)
//             {
//                 dtRefresh = userToken.Expiration.Value.Subtract(_options.RefreshBeforeExpiration);
//             }

//             if (dtRefresh < _clock.UtcNow || parameters.ForceRenewal)
//             {
//                 _logger.LogDebug("Token for user {user} needs refreshing.", userName);

//                 try
//                 {
//                     return await _sync.Dictionary.GetOrAdd(userToken.RefreshToken, _ =>
//                     {
//                         return new Lazy<Task<string>>(async () =>
//                         {
//                             var refreshed = await RefreshUserAccessTokenAsync(user, parameters, cancellationToken);
//                             return refreshed.AccessToken;
//                         });
//                     }).Value;
//                 }
//                 finally
//                 {
//                     _sync.Dictionary.TryRemove(userToken.RefreshToken, out _);
//                 }
//             }

//             return userToken.AccessToken;
//         }

//         private async Task<UserAccessToken> GetTokenAsync(ClaimsPrincipal user, TokenParameters parameters)
//         {
//             var result = await _contextAccessor.HttpContext.AuthenticateAsync(parameters.SignInScheme);

//             if (!result.Succeeded)
//             {
//                 _logger.LogInformation("Cannot authenticate scheme: {scheme}", parameters.SignInScheme ?? "default signin scheme");
//                 return null;
//             }

//             if (result.Properties == null)
//             {
//                 _logger.LogInformation("Authentication result properties are null for scheme: {scheme}",
//                     parameters.SignInScheme ?? "default signin scheme");

//                 return null;
//             }

//             var tokens = result.Properties.Items.Where(i => i.Key.StartsWith(TokenPrefix)).ToList();
//             if (tokens == null || !tokens.Any())
//             {
//                 _logger.LogInformation("No tokens found in cookie properties. SaveTokens must be enabled for automatic token refresh.");

//                 return null;
//             }

//             var tokenName = $"{TokenPrefix}{OpenIdConnectParameterNames.AccessToken}";
//             if (!string.IsNullOrEmpty(parameters.Resource))
//             {
//                 tokenName += $"::{parameters.Resource}";
//             }

//             var expiresName = $"{TokenPrefix}expires_at";
//             if (!string.IsNullOrEmpty(parameters.Resource))
//             {
//                 expiresName += $"::{parameters.Resource}";
//             }

//             var accessToken = tokens.SingleOrDefault(t => t.Key == tokenName);
//             var refreshToken = tokens.SingleOrDefault(t => t.Key == $"{TokenPrefix}{OpenIdConnectParameterNames.RefreshToken}");
//             var expiresAt = tokens.SingleOrDefault(t => t.Key == expiresName);

//             DateTimeOffset? dtExpires = null;
//             if (expiresAt.Value != null)
//             {
//                 dtExpires = DateTimeOffset.Parse(expiresAt.Value, CultureInfo.InvariantCulture);
//             }

//             return new UserAccessToken
//             {
//                 AccessToken = accessToken.Value,
//                 RefreshToken = refreshToken.Value,
//                 Expiration = dtExpires
//             };
//         }

//         /// <inheritdoc />
//         public async Task StoreTokenAsync(ClaimsPrincipal user, string accessToken, DateTimeOffset expiration,
//            string refreshToken = null, TokenParameters parameters = null)
//         {
//             parameters ??= new TokenParameters();
//             var result = await _contextAccessor.HttpContext.AuthenticateAsync(parameters.SignInScheme);

//             if (!result.Succeeded)
//             {
//                 throw new Exception("Can't store tokens. User is anonymous");
//             }

//             var tokenName = OpenIdConnectParameterNames.AccessToken;
//             if (!string.IsNullOrEmpty(parameters.Resource))
//             {
//                 tokenName += $"::{parameters.Resource}";
//             }

//             var expiresName = "expires_at";
//             if (!string.IsNullOrEmpty(parameters.Resource))
//             {
//                 expiresName += $"::{parameters.Resource}";
//             }

//             result.Properties.Items[$".Token.{tokenName}"] = accessToken;
//             result.Properties.Items[$".Token.{expiresName}"] = expiration.ToString("o", CultureInfo.InvariantCulture);

//             if (refreshToken != null)
//             {
//                 result.Properties.UpdateTokenValue(OpenIdConnectParameterNames.RefreshToken, refreshToken);
//             }

//             await _contextAccessor.HttpContext.SignInAsync(parameters.SignInScheme, user, result.Properties);
//         }

//         /// <inheritdoc/>
//         public async Task RevokeRefreshTokenAsync(
//             ClaimsPrincipal user,
//             TokenParameters parameters = null,
//             CancellationToken cancellationToken = default)
//         {
//             parameters ??= new TokenParameters();
//             var userToken = await GetTokenAsync(user, parameters);

//             if (!string.IsNullOrEmpty(userToken?.RefreshToken))
//             {
//                 var scheme = parameters.ChallengeScheme;
//                 var response = await _tokenEndpointService.RevokeRefreshTokenAsync(userToken.RefreshToken, parameters, cancellationToken);

//                 if (response.IsError)
//                 {
//                     _logger.LogError("Error revoking refresh token. Error = {error}", response.Error);
//                 }
//             }
//         }

//         private async Task<TokenResponse> RefreshUserAccessTokenAsync(ClaimsPrincipal user, TokenParameters parameters, CancellationToken cancellationToken = default)
//         {
//             var userToken = await GetTokenAsync(user, parameters);
//             var response = await _tokenEndpointService.RefreshUserAccessTokenAsync(userToken.RefreshToken, parameters, cancellationToken);

//             if (!response.IsError)
//             {
//                 var expiration = DateTime.UtcNow + TimeSpan.FromSeconds(response.ExpiresIn);

//                 await StoreTokenAsync(user, response.AccessToken, expiration, response.RefreshToken, parameters);
//             }
//             else
//             {
//                 _logger.LogError("Error refreshing access token. Error = {error}", response.Error);
//             }

//             return response;
//         }
//     }
// }