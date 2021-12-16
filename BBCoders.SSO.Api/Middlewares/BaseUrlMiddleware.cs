using System.Threading.Tasks;
using BBCoders.SSO.Api.Extensions;
using BBCoders.SSO.ApiModels.Constants;
using Microsoft.AspNetCore.Http;

namespace BBCoders.SSO.Api.Middlewares
{
    public class BaseUrlMiddleware
    {
        private readonly RequestDelegate _next;

        public BaseUrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            var baseUrl = request.PathBase.Value.RemoveTrailingSlash();

            context.Items[EnvironmentKeys.IdentityServerBasePath] = baseUrl; 

            await _next(context);
        }
    }
}