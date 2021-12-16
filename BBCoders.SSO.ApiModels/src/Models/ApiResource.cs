using System.Collections.Generic;

namespace BBCoders.SSO.ApiModels
{
    /// <summary>
    /// Models a web API resource.
    /// </summary>
    public class ApiResource : Resource
    { 
        /// <summary>
        /// The API secret is used for the introspection endpoint. The API can authenticate with introspection using the API name and secret.
        /// </summary>
        public ICollection<Secret> ApiSecrets { get; set; } = new HashSet<Secret>();

        /// <summary>
        /// Models the scopes this API resource allows.
        /// </summary>
        public ICollection<string> Scopes { get; set; } = new HashSet<string>();

        /// <summary>
        /// Signing algorithm for access token. If empty, will use the server default signing algorithm.
        /// </summary>
        public ICollection<string> AllowedAccessTokenSigningAlgorithms { get; set; } = new HashSet<string>();
    }
}
