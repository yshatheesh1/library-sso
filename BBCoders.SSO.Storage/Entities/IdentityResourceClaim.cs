

namespace BBCoders.SSO.Storage
{
    public class IdentityResourceClaim : UserClaim
    {
        public int IdentityResourceId { get; set; }
        public IdentityResource IdentityResource { get; set; }
    }
}