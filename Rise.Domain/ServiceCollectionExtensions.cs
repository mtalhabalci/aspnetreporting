using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SDIKit.Common;
using SDIKit.Data;

namespace Rise.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationDbContext(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

            services.AddSqlDb<RiseDbContext>(configuration, k => k.UseNpgsql(databaseSettings.ConnectionString, k => k.MigrationsHistoryTable("__MigrationsHistory", databaseSettings.DefaultScheme)));
        }
    }
}
