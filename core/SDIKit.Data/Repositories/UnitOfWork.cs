using SDIKit.Common.Entity;
using SDIKit.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SDIKit.Data.Repositories
{
    public class UnitOfWork : IRepositoryFactory, IUnitOfWork
    {
        private Dictionary<string, object> repositories;
        private bool disposed = false;

        public UnitOfWork(IDbContextBase context)
        {
            DataContext = context ?? throw new ArgumentNullException(nameof(context));
            repositories = new Dictionary<string, object>();
        }

        public IDbContextBase DataContext { get; }

        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return DataContext.BeginTransaction(isolationLevel);
        }

        public bool CanConnect()
        {
            return ((DbContext)DataContext).Database.CanConnect();
        }

        public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
        {
            return await ((DbContext)DataContext).Database.CanConnectAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            DataContext.Dispose();
            GC.Collect(2, GCCollectionMode.Forced);
            GC.SuppressFinalize(this);
            GC.SuppressFinalize(DataContext);
        }

        public T ExecuteScalar<T>(string sql)
        {
            return DataContext.ExecuteScalar<T>(sql);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return ((DbContext)DataContext).Database.ExecuteSqlRaw(sql, parameters);
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : EntityBase
        {
            return ((DbContext)DataContext).Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            var typeName = typeof(TEntity).Name;
            if (repositories.ContainsKey(typeName))
            {
                return (Repository<TEntity>)repositories[typeName];
            }

            var instance = new Repository<TEntity>(DataContext);
            repositories.Add(typeName, instance);
            return instance;
        }

        public int SaveChanges()
        {
            return DataContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await DataContext.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }
                    DataContext.Dispose();
                }
            }

            disposed = true;
        }
    }
}