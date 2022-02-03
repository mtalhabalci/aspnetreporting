using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rise.Domain;
using SDIKit.Common;
using SDIKit.Common.Configurations;
using SDIKit.Common.Helpers;
using SDIKit.Data;
using SDIKit.Data.Interfaces;
using System.IO;

namespace Rise.Data.Test
{
    public class UnitTestBase
    {
        protected ServiceProvider Provider { get; }
        protected IUnitOfWork UnitOfWork { get; }
        protected LipsumGeneratorHelper LoremIpsum { get; }

        public UnitTestBase()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(k =>
            {
                return new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
            });
            var configuration = AppConfigurations.Get(Path.Combine(Directory.GetCurrentDirectory()));
            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSqlDb<RiseDbContext>(configuration, k => k.UseNpgsql("Host=127.0.0.1;Port=5432;Database=Rise_Dev;Username=postgres;Password=123456;Persist Security Info=true;", k => k.MigrationsHistoryTable("__MigrationsHistory", "public")));

            Provider = services.BuildServiceProvider();
            UnitOfWork = Provider.GetService<IUnitOfWork>();
            LoremIpsum = new LipsumGeneratorHelper();
        }
    }
}
