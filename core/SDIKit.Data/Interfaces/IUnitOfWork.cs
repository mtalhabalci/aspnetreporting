using SDIKit.Common.Entity;

using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SDIKit.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContextBase DataContext { get; }

        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        bool CanConnect();

        Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);

        T ExecuteScalar<T>(string sql);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : EntityBase;

        IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}