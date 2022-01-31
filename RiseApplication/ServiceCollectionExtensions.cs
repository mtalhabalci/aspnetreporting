using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rise.Application.Contracts.Managers;
using Rise.Application.Contracts.Managers.ContactInformation;
using Rise.Application.Contracts.Managers.Report;
using Rise.Application.Managers;
using Rise.Domain;
using SDIKit.Common;
using SDIKit.Data;
using System;

namespace Rise.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IContactInformationManager, ContactInformationManager>();
            services.AddScoped<IReportManager, ReportManager>();
            services.AddApplicationDbContext(configuration);
        }
    }
}
