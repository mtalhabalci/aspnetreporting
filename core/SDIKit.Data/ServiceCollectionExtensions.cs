using SDIKit.Data.Interfaces;
using SDIKit.Data.Repositories;
using SDIKit.Data.Types;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace SDIKit.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSqlDb<TContext>(this IServiceCollection services, IConfiguration configuration, Action<DbContextOptionsBuilder> dbContextOptionsBuilder) where TContext : DbContextBase
        {
            services.AddDbContext<TContext>(dbContextOptionsBuilder);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbContextBase, TContext>();
        }
    }
}