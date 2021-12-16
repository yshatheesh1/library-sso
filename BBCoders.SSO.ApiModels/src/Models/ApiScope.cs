namespace BBCoders.SSO.ApiModels
{
    /// <summary>
    /// api scope
    /// </summary>
    public class ApiScope : Resource
    {
        /// <summary>
        /// Specifies whether the user can de-select the scope on the consent screen. Defaults to false.
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// Specifies whether the consent screen will emphasize this scope. Use this setting for sensitive or important scopes. Defaults to false.
        /// </summary>
        public bool Emphasize { get; set; } = false;
    }
}
