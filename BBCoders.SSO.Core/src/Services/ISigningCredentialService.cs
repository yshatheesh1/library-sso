using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BBCoders.SSO.Core.src.Services
{
    /// <summary>
    /// Interface for a signing credential store
    /// </summary>
    public interface ISigningCredentialService
    {
        /// <summary>
        /// Gets the signing credentials.
        /// </summary>
        /// <returns></returns>
        Task<SigningCredentials> GetSigningCredentialsAsync();
    }
}