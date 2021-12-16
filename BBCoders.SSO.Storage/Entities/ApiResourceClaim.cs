namespace BBCoders.SSO.Storage
{
    public class ApiResourceClaim : UserClaim
    {
        public int ApiResourceId { get; set; }
        public ApiResource ApiResource { get; set; }
    }
}