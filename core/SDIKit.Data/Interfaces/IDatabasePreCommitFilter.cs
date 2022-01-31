using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Collections.Generic;

namespace SDIKit.Data.Interfaces
{
    public interface IDatabasePreCommitFilter
    {
        void Invoke(IEnumerable<EntityEntry> entries);
    }
}