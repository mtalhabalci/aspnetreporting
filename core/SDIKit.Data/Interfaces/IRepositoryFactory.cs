using SDIKit.Common.Entity;

namespace SDIKit.Data.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
    }
}