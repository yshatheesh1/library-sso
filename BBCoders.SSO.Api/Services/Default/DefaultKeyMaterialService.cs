using System;
using System.Collections.Generic;
using System.Linq;
using BBCoders.SSO.Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BBCoders.SSO.Api.Services
{
  /// <summary>
    /// The default key material service
    /// </summary>
    public class DefaultKeyMaterialService : IKeyMaterialService
    {
        private readonly IEnumerable<SigningCredentials> _signingCredentials;
        private readonly Dictionary<string, SecurityKey> _securityKeyInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultKeyMaterialService"/> class.
        /// </summary>
        /// <param name="securityKeyInfos">The security keys.</param>
        /// <param name="signingCredentials">The signing credential.</param>
        public DefaultKeyMaterialService(Dictionary<string, SecurityKey> securityKeyInfos, IEnumerable<SigningCredentials> signingCredentials)
        {
            _signingCredentials = signingCredentials;
            _securityKeyInfos = securityKeyInfos;
        }

        /// <inheritdoc/>
        public SigningCredentials GetSigningCredentials(IEnumerable<string> allowedAlgorithms = null)
        {
            if (_signingCredentials.Any())
            {
                if (allowedAlgorithms.IsNullOrEmpty())
                {
                    return _signingCredentials.First();
                }

                var credential = _signingCredentials.FirstOrDefault(c => allowedAlgorithms.Contains(c.Algorithm));
                if (credential is null)
                {
                    throw new InvalidOperationException($"No signing credential for algorithms ({string.Join(",", allowedAlgorithms)}) registered.");
                }

                return credential;
            }

            return null;
        }

        /// <inheritdoc/>
        public IEnumerable<SigningCredentials> GetAllSigningCredentials()
        {
            var credentials = new List<SigningCredentials>(_signingCredentials); 
            return credentials;
        }

        /// <inheritdoc/>
        public Dictionary<string, SecurityKey> GetValidationKeys()
        {
            var keys = new Dictionary<string, SecurityKey>(_securityKeyInfos); 
            return keys;
        }
    }
}