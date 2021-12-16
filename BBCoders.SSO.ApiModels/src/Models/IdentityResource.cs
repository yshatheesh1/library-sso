namespace BBCoders.SSO.ApiModels
{
    /// <summary>
    /// Models a user identity resource.
    /// </summary>
    public class IdentityResource : Resource
    {

        /// <summary>
        /// Specifies whether the user can de-select the scope on the consent screen (if the consent screen wants to implement such a feature). Defaults to false.
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// Specifies whether the consent screen will emphasize this scope (if the consent screen wants to implement such a feature). 
        /// Use this setting for sensitive or important scopes. Defaults to false.
        /// </summary>
        public bool Emphasize { get; set; } = false;
    }
}
