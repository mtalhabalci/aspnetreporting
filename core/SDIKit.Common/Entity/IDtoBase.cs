using System;

namespace SDIKit.Common.Entity
{
    public interface IDtoBase<TKey> : IModelKey<long>
    {
        DateTime Created { get; set; }
        long? CreatedBy { get; set; }
        DateTime? Modified { get; set; }
        long? ModifiedBy { get; set; }
        bool IsActive { get; set; }
    }
}