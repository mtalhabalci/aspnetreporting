using SDIKit.Common.Entity;

using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SDIKit.Data.Interfaces
{
    public interface IDbContextBase : IDisposable
    {
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : IEntity;
        void SyncBulkObjectState<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity;

        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        T ExecuteScalar<T>(string sql);

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}