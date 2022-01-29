using SDIKit.Common.Enums;

using Microsoft.EntityFrameworkCore;

using System;

namespace SDIKit.Data.Helpers
{
    public class StateHelper
    {
        public static EntityState ConvertState(ObjectState state)
        {
            return state switch
            {
                ObjectState.Added => EntityState.Added,
                ObjectState.Modified => EntityState.Modified,
                ObjectState.Deleted => EntityState.Deleted,
                _ => EntityState.Unchanged,
            };
        }

        public static ObjectState ConvertState(EntityState state)
        {
            return state switch
            {
                EntityState.Added => ObjectState.Added,
                EntityState.Deleted => ObjectState.Deleted,
                EntityState.Detached => ObjectState.Unchanged,
                EntityState.Modified => ObjectState.Modified,
                EntityState.Unchanged => ObjectState.Unchanged,
                _ => throw new ArgumentOutOfRangeException("Improper Entity State"),
            };
        }
    }
}