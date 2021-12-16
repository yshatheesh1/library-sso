using System.Collections.Generic;
using System.IO;
using System.Linq;
using BBCoders.Commons.QueryConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BBCoders.SSO.Storage
{
    public class ResourceQueryConfiguration : IQueryConfiguration<SSOContext>
    {
        public void CreateQuery(SSOContext context, QueryOperations queryOperations)
        {
            queryOperations.Add("GetAllIdentityResourcesAsync", () =>
                 context.IdentityResources
                .Include(x => x.UserClaims)
                .Include(x => x.Properties));

            queryOperations.Add("GetAllApiResourcesAsync", () =>
                context.ApiResources
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties));

            queryOperations.Add("GetAllApiScopesAsync", () =>
                context.ApiScopes
                .Include(x => x.UserClaims)
                .Include(x => x.Properties));

            queryOperations.Add<IEnumerable<string>>("GetApiResourcesByNameAsync", (apiResourceNames) =>
                context.ApiResources.Where(x => apiResourceNames.Contains(x.Name))
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties));

            queryOperations.Add<IEnumerable<string>>("GetApiResourcesByScopeNameAsync", (scopeNames) =>
                context.ApiResources.Where(x => x.Scopes.Where(x => scopeNames.Contains(x.Scope)).Any())
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties));

            queryOperations.Add<IEnumerable<string>>("GetApiScopesByNameAsync", (scopeNames) =>
                context.ApiScopes.Where(x => scopeNames.Contains(x.Name))
                .Include(x => x.UserClaims)
                .Include(x => x.Properties));
        }

        public QueryOptions GetQueryOptions()
        {
            return new QueryOptions()
            {
                PackageName = "BBCoders.SSO.Storage",
                ClassName = "ResourceRepository",
                FileExtension = "cs",
                FileName = "ResourceRepository",
                Language = "csharp",
                ModelFileName = "ResourceModel",
                OutputDirectory = Path.Join(Directory.GetCurrentDirectory(), "Repository")
            };
        }
    }
}