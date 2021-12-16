using System.Threading.Tasks;
using BBCoders.SSO.Core.src.Models;

namespace BBCoders.SSO.Core.Services
{
        /// <summary>
    /// Interface the token response generator
    /// </summary>
    public interface TokenGenerator
    {
        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="tokenRequestDTO">The token request.</param>
        /// <returns></returns>
        Task<TokenResponseDTO> ProcessAsync(TokenRequestDTO tokenRequestDTO);
    }
}