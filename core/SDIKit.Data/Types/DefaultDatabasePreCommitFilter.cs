using SDIKit.Data.Interfaces;

using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Collections.Generic;

namespace SDIKit.Data.Types
{
    public class DefaultDatabasePreCommitFilter : IDatabasePreCommitFilter
    {
        public DefaultDatabasePreCommitFilter()
        {
        }

        public void Invoke(IEnumerable<EntityEntry> entries)
        {
        }
    }
}