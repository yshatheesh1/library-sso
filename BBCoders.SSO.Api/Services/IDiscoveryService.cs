using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBCoders.SSO.Api.Services
{
    public interface IDiscoveryService
    {
        Task<Dictionary<string, object>> CreateDiscoveryDocumentAsync(string baseUrl);
    }
}