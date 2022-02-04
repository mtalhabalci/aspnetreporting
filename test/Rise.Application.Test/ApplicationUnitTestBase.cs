using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rise.Application.Contracts.Managers;
using Rise.Application.Contracts.Managers.ContactInformation;
using Rise.Application.Contracts.Managers.Report;
using Rise.Application.Managers;
using Rise.Domain;
using Rise.Rabbitmq;
using SDIKit.Common;
using SDIKit.Common.Configurations;
using SDIKit.Common.Helpers;
using SDIKit.Data;
using SDIKit.Data.Interfaces;
using System.IO;

namespace Rise.Application.Test
{
    public class ApplicationUnitTestBase
    {
        protected ServiceProvider Provider { get; }
        protected IUnitOfWork UnitOfWork { get; }
        protected LipsumGeneratorHelper LoremIpsum { get; }

        protected IPersonManager PersonManager { get; }
        protected IContactInformationManager ContactInformationManager { get; }
        protected IReportManager ReportManager { get; }

        protected IRabbitmqPost RabbitmqPost { get; }
        public ApplicationUnitTestBase()
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

            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IContactInformationManager, ContactInformationManager>();
            services.AddScoped<IReportManager, ReportManager>();
            services.AddScoped<IRabbitmqPost, RabbitmqPost>();

            Provider = services.BuildServiceProvider();
            UnitOfWork = Provider.GetService<IUnitOfWork>();
            PersonManager = Provider.GetService<IPersonManager>();
            ContactInformationManager = Provider.GetService<IContactInformationManager>();
            ReportManager = Provider.GetService<IReportManager>();
            RabbitmqPost = Provider.GetService<IRabbitmqPost>();
            LoremIpsum = new LipsumGeneratorHelper();
        }
    }
}
