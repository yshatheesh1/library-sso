using System.IO;
using BBCoders.Commons.QueryConfiguration;

namespace BBCoders.SSO.Storage.Queries
{
    public class CRUDQueryConfiguration : IQueryConfiguration<SSOContext>
    {
        public void CreateQuery(SSOContext context, QueryOperations queryOperations)
        {
            queryOperations.Add<Client>();
            queryOperations.Add<ClientCorsOrigin>();
            queryOperations.Add<IdentityResource>();
            queryOperations.Add<ApiResource>();
            queryOperations.Add<ApiScope>();
            queryOperations.Add<PersistedGrant>();
            queryOperations.Add<DeviceFlowCodes>();
        }

        public QueryOptions GetQueryOptions()
        {
            return new QueryOptions()
            {
                PackageName = "BBCoders.SSO.Storage",
                ClassName = "BaseRepository",
                FileExtension = "cs",
                FileName = "BaseRepository",
                Language = "csharp",
                ModelFileName = "BaseRepositoryModel",
                OutputDirectory = Path.Join(Directory.GetCurrentDirectory(), "Repository")
            };
        }
    }
}