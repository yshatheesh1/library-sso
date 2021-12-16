using System.Collections.Generic;

namespace BBCoders.SSO.ApiModels
{
    /// <summary>
    /// Models a collection of identity and API resources.
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// Gets or sets a value indicating whether [offline access].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [offline access]; otherwise, <c>false</c>.
        /// </value>
        public bool OfflineAccess { get; set; }

        /// <summary>
        /// Gets or sets the identity resources.
        /// </summary>
        public ICollection<IdentityResource> IdentityResources { get; set; } = new HashSet<IdentityResource>();

        /// <summary>
        /// Gets or sets the API resources.
        /// </summary>
        public ICollection<ApiResource> ApiResources { get; set; } = new HashSet<ApiResource>();
        
        /// <summary>
        /// Gets or sets the API scopes.
        /// </summary>
        public ICollection<ApiScope> ApiScopes { get; set; } = new HashSet<ApiScope>();
    }
}
