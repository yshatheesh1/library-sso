using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BBCoders.SSO.Core.Models;

namespace BBCoders.SSO.Core.Services
{
    /// <summary>
    /// abstraction for managing user token
    /// </summary>
    public interface TokenService
    {
        /// <summary>
        /// Gets user token from principal
        /// </summary>
        /// <param name="user">user principal</param>
        /// <param name="parameters">parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>access token</returns>
        Task<string> GetUserAccessTokenAsync(
            ClaimsPrincipal user,
            TokenParameters parameters = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// stores user token from principal
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accessToken"></param>
        /// <param name="expiration"></param>
        /// <param name="refreshToken"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task StoreTokenAsync(
            ClaimsPrincipal user,
            string accessToken,
            DateTimeOffset expiration,
            string refreshToken = null,
            TokenParameters parameters = null);
    }
}