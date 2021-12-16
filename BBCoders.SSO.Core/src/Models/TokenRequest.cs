namespace BBCoders.SSO.Core.src.Models
{
    /// <summary>
    /// token request
    /// </summary>
    public class TokenRequestDTO
    {
        /// <summary>
        /// get or set grant type
        /// </summary>
        /// <value></value>
        public string GrantType;

        public string AuthorizationCode { get; set; }
    }
}