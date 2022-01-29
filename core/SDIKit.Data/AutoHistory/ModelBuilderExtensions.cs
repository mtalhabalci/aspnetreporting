﻿using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using System;

namespace SDIKit.Data.AutoHistory
{
    /// <summary>
    /// Represents a plugin for Microsoft.EntityFrameworkCore to support automatically recording data changes history.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        private const int DefaultChangedMaxLength = 2048;

        /// <summary>
        /// Enables the automatic recording change history.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> to enable auto history feature.</param>
        /// <param name="changedMaxLength">The maximum length of the 'Changed' column. <c>null</c> will use default setting 2048.</param>
        /// <returns>The <see cref="ModelBuilder"/> had enabled auto history feature.</returns>
        public static ModelBuilder EnableAutoHistory(this ModelBuilder modelBuilder, int? changedMaxLength)
        {
            return ModelBuilderExtensions.EnableAutoHistory<AutoHistory>(modelBuilder, o =>
            {
                o.ChangedMaxLength = changedMaxLength;
                o.LimitChangedLength = false;
            });
        }

        public static ModelBuilder EnableAutoHistory<TAutoHistory>(this ModelBuilder modelBuilder, Action<AutoHistoryOptions> configure)
            where TAutoHistory : AutoHistory
        {
            var options = AutoHistoryOptions.Instance;
            configure?.Invoke(options);
            options.JsonSerializer = JsonSerializer.Create(options.JsonSerializerSettings);

            modelBuilder.Entity<TAutoHistory>(b =>
            {
                b.Property(c => c.RowId).IsRequired().HasMaxLength(options.RowIdMaxLength);
                b.Property(c => c.TableName).IsRequired().HasMaxLength(options.TableMaxLength);

                if (options.LimitChangedLength)
                {
                    var max = options.ChangedMaxLength ?? DefaultChangedMaxLength;
                    if (max <= 0)
                    {
                        max = DefaultChangedMaxLength;
                    }
                    b.Property(c => c.Changed).HasMaxLength(max);
                }

                // This MSSQL only
                //b.Property(c => c.Created).HasDefaultValueSql("getdate()");
            });

            return modelBuilder;
        }
    }
}