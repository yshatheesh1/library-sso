

namespace BBCoders.SSO.Storage
{
    public class ClientSecret : Secret
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}