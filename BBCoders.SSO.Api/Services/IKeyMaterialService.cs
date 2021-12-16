using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace BBCoders.SSO.Api.Services
{
    /// <summary>
    /// Interface for the key material service
    /// </summary>
    public interface IKeyMaterialService
    {
        /// <summary>
        /// Gets all validation keys.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, SecurityKey> GetValidationKeys();

        /// <summary>
        /// Gets the signing credentials.
        /// </summary>
        /// <param name="allowedAlgorithms">Collection of algorithms used to filter the server supported algorithms. 
        /// A value of null or empty indicates that the server default should be returned.</param>
        /// <returns></returns>
        SigningCredentials GetSigningCredentials(IEnumerable<string> allowedAlgorithms = null);

        /// <summary>
        /// Gets all signing credentials.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SigningCredentials> GetAllSigningCredentials();
    }
}