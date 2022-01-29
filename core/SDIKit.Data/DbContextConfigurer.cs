using Microsoft.EntityFrameworkCore;

using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

using System;

namespace SDIKit.Data
{
    public static class DbContextConfigurer
    {
        public static void Configure(this DbContextOptionsBuilder builder, string connectionString, Action<NpgsqlDbContextOptionsBuilder> npgsqlOptionsAction = null)
        {
            if (!builder.IsConfigured)
            {
                if (connectionString.Contains("sqlite"))
                    builder.UseSqlite(connectionString);
                else
                    builder.UseNpgsql(connectionString, npgsqlOptionsAction);
            }
        }
    }
}