using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rise.Domain;
using SDIKit.Common;
using SDIKit.Common.Configurations;
using SDIKit.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Migrator
{
    internal class Migrator
    {
        private ServiceProvider Provider { get; }

        public DatabaseSettings GetDbSettings()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = AppConfigurations.Get(Path.Combine(Directory.GetCurrentDirectory()), environmentName: envName);

            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

            return databaseSettings;
        }

        public Migrator()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var services = new ServiceCollection();
            var supportedCulture = new List<CultureInfo>
            {
                new CultureInfo("tr-TR"),
                new CultureInfo("tr"),
                new CultureInfo("en-US"),
                new CultureInfo("en")
            };

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                opts.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"));
                opts.SupportedCultures = supportedCulture;
                opts.SupportedUICultures = supportedCulture;
            });
            var configuration = AppConfigurations.Get(Path.Combine(Directory.GetCurrentDirectory()), environmentName: envName);

            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

            services.AddSqlDb<RiseDbContext>(configuration, k => k.UseNpgsql(databaseSettings.ConnectionString, k => k.MigrationsHistoryTable("__MigrationsHistory", databaseSettings.DefaultScheme)));

            Provider = services.BuildServiceProvider();
        }

        public async Task<bool> EnsureCreated()
        {
            return await InitializeTelepatiDatabaseAsync(Provider);
        }

        

        private async Task<bool> InitializeTelepatiDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<RiseDbContext>();

                var DatabaseExist = await db.Database.EnsureCreatedAsync();
                if (DatabaseExist)
                {
                }
                return DatabaseExist;
            }
        }      
    }
}
