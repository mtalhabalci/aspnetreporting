namespace SDIKit.Common.Entity
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TKey">Entity key type parameter</typeparam>
    public interface IEntityIdentity<TKey> : IEntity, IModelKey<TKey>
    {
    }
}