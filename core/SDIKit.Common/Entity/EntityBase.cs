using System;

namespace SDIKit.Common.Entity
{
    public abstract class EntityBase : EntityIdentityBase, IEntityWithDeleted
    {
        protected EntityBase()
        {
        }

        public bool IsDeleted { get; set; }
    }
}