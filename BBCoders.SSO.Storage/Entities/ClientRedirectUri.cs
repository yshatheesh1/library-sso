﻿

namespace BBCoders.SSO.Storage
{
    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public string RedirectUri { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}