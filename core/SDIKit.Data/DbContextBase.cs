using SDIKit.Common;
using SDIKit.Common.Entity;
using SDIKit.Common.Identity;
using SDIKit.Data.AutoHistory;
using SDIKit.Data.Helpers;
using SDIKit.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace SDIKit.Data
{
    public class DbContextBase : DbContext, IDbContextBase
    {
        private readonly DatabaseSettings _databaseSettings;

        private bool _disposed;

        public DbContextBase()
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public DbContextBase([NotNullAttribute] DbContextOptions options, IOptions<DatabaseSettings> databaseSettings) : base(options)
        {
            _databaseSettings = databaseSettings.Value;

            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_databaseSettings.DefaultScheme);
            modelBuilder.EnableAutoHistory(int.MaxValue);
            base.OnModelCreating(modelBuilder);
        }

        [DebuggerStepThrough]
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T ExecuteScalar<T>(string sql)
        {
            var connection = Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                var resultObj = command.ExecuteScalar();
                connection.Close();
                return (T)resultObj;
            }
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlRaw(sql, parameters);
        }

        public override int SaveChanges()
        {
            if (_databaseSettings.EnableAutoHistory)
                ChangeTracker.Context.EnsureAutoHistory();
            int changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_databaseSettings.EnableAutoHistory)
                ChangeTracker.Context.EnsureAutoHistory();
            int changes = await base.SaveChangesAsync(cancellationToken);
            SyncObjectsStatePostCommit();
            return changes;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : IEntity
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public void SyncBulkObjectState<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity
        {
            entities.Select(entity => { Entry(entity).State = StateHelper.ConvertState(entity.ObjectState); return entity; }).ToList();
        }

        /// <summary>
        ///     Disposes the DbContext.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // free other managed objects that implement
                    // IDisposable only
                }
            }
            _disposed = true;
        }

        private void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                var objectStateEntry = dbEntityEntry.Entity as IEntity;

                if (objectStateEntry != null)
                {
                    objectStateEntry.ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
                }
            }
        }

       

        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Database.BeginTransaction(isolationLevel);
        }
    }
}