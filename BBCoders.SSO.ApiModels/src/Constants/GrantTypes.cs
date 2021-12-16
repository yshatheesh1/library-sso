using System.Collections.Generic;

namespace BBCoders.SSO.Core
{
    /// <summary>
    /// grant types
    /// </summary>
    public static class GrantType
    {
        /// <summary>
        /// implicit grant type is optimized for browser-based applications
        /// </summary>
        public const string Implicit = "implicit";
        /// <summary>
        /// it uses combinations of multiple grant type
        /// </summary>
        public const string Hybrid = "hybrid";
        /// <summary>
        /// provides a way to retrieve tokens on a back-channel as opposed to the browser front-channel.
        /// </summary>
        public const string AuthorizationCode = "authorization_code";
        /// <summary>
        /// used for server to server communication 
        /// </summary>
        public const string ClientCredentials = "client_credentials";
        /// <summary>
        /// used for request tokens on behalf of a user by sending the user’s name and password to the token endpoint.
        /// </summary>
        public const string ResourceOwnerPassword = "password";
        /// <summary>
        /// This flow is typically used by IoT devices and can request both identity and API resources.
        /// </summary>
        public const string DeviceFlow = "urn:ietf:params:oauth:grant-type:device_code";
    }
}
