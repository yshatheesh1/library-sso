using System;
using System.IO;
using System.Linq;
using BBCoders.Commons.QueryConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BBCoders.SSO.Storage.Queries
{
    public class ClientQueryConfiguration : IQueryConfiguration<SSOContext>
    {
        public void CreateQuery(SSOContext context, QueryOperations queryOperations)
        {
            queryOperations.Add<string>("GetClientByIdAsync", (clientId) =>
                context.Clients.Where(x => x.ClientId == clientId)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.AllowedScopes)
                .Include(x => x.Claims)
                .Include(x => x.ClientSecrets)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.Properties)
                .Include(x => x.RedirectUris));
        }

        public QueryOptions GetQueryOptions()
        {
            return new QueryOptions()
            {
                PackageName = "BBCoders.SSO.Data",
                ClassName = "ClientRepository",
                FileExtension = "cs",
                FileName = "ClientRepository",
                Language = "csharp",
                ModelFileName = "ClientModel",
                OutputDirectory = Path.Join(Directory.GetCurrentDirectory(), "Repository")
            };
        }

    }
}