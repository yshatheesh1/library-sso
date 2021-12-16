using Microsoft.AspNetCore.Http;

namespace BBCoders.SSO.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetHostAddress(this HttpContext context)
        {
            var request = context.Request;
            return request.Scheme + "://" + request.Host.ToUriComponent();
        }

        public static string GetBasePath(this HttpContext context)
        {
            return context.Request.PathBase.Value.RemoveTrailingSlash();
        }

        public static string GetBaseUrl(this HttpContext context)
        {
            return context.GetHostAddress() + context.GetBasePath();
        }
    }
}