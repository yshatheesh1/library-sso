using System.Diagnostics;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;

namespace BBCoders.SSO.Storage
{
    public class DesignTimeBuilder : IDesignTimeDbContextFactory<SSOContext>
    {
        public static string ConnectionString = "Server=localhost;port=3306;database=usermanagement;uid=usermanagement_test;pwd=usermanagement_test;";

        public SSOContext CreateDbContext(string[] args)
        {
            //             while(!Debugger.IsAttached){
            //     Thread.Sleep(300);
            // }
            var builder = new DbContextOptionsBuilder<SSOContext>().UseMySQL(ConnectionString);
            var testContext = new SSOContext(builder.Options);
            return testContext;
        }
    }
    public class MyDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkMySQL();
        }
    }
}