namespace SDIKit.Common.Entity
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TKey">Entity key type parameter</typeparam>
    public interface IModelKey<TKey>
    {
        TKey Id { get; set; }
    }
}