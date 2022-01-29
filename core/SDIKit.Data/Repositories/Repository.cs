using SDIKit.Common.Entity;
using SDIKit.Common.Enums;
using SDIKit.Common.Interfaces;
using SDIKit.Data.Extensions;
using SDIKit.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SDIKit.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly IDbContextBase _dataContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(IDbContextBase context)
        {
            _dataContext = context;
            var dbContext = context as DbContext;
            if (dbContext == null)
                throw new InvalidOperationException(nameof(dbContext));

            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            var now = DateTime.Now;
            entity.ObjectState = ObjectState.Added;
            _dbSet.Attach(entity);
            _dataContext.SyncObjectState(entity);
        }

        public virtual void BulkInsert(IEnumerable<TEntity> entities)
        {
            var now = DateTime.Now;

            _dbSet.AddRangeAsync(entities);
            _dataContext.SyncBulkObjectState(entities);
        }

        public virtual void Update(TEntity entity)
        {
            var now = DateTime.Now;
            entity.ObjectState = ObjectState.Modified;
            _dbSet.Attach(entity);
            _dataContext.SyncObjectState(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(params TEntity[] entities)
        {
            Delete(entities.ToList());
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        #region [Methods]

        public virtual async Task<TEntity> FindAsync(long id,
            CancellationToken cancellationToken = default)
        {
            return await GetFirstAsync(i => i.Id == id, cancellationToken: cancellationToken);
        }

        public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _dbSet.FromSqlRaw<TEntity>(sql, parameters);
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate == null) predicate = x => true;
            return await query.Where(predicate).AnyAsync(cancellationToken);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate == null) predicate = x => true;
            return await query.Where(predicate).CountAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null) predicate = x => true;
            return _dbSet.Where(predicate);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate == null) predicate = x => true;
            return await query.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<TResult>> GetAndMapAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            , CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate == null) predicate = i => true;
            return await query.Where(predicate).Select(selector).ToListAsync(cancellationToken);
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).FirstOrDefault();
            return query.FirstOrDefault();
        }

        public virtual TResult GetFirstAndMap<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).Select(selector).FirstOrDefault();
            return query.Select(selector).FirstOrDefault();
        }

        public virtual async Task<TResult> GetFirstAndMapAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).Select(selector).FirstOrDefaultAsync(cancellationToken);
            return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).FirstOrDefaultAsync(cancellationToken);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).FirstOrDefault();
            return query.SingleOrDefault();
        }

        public virtual TResult GetSingleAndMap<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).Select(selector).FirstOrDefault();
            return query.Select(selector).SingleOrDefault();
        }

        public virtual async Task<TResult> GetSingleAndMapAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).Select(selector).FirstOrDefaultAsync(cancellationToken);
            return await query.Select(selector).SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).FirstOrDefaultAsync(cancellationToken);
            return await query.SingleOrDefaultAsync(cancellationToken);
        }

        public virtual IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0,
            int pageSize = 20)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).ToPagedList(pageIndex, pageSize);
            return query.ToPagedList(pageIndex, pageSize);
        }

        public virtual IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0,
            int pageSize = 20) where TResult : class
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).Select(selector).ToPagedList(pageIndex, pageSize);
            return query.Select(selector).ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0,
            int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            return query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        public virtual Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
            where TResult : class
        {
            var query = _dbSet.AsQueryable();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query.Select(selector)).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        #endregion [Methods]
    }
}