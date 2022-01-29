using System.Collections.Generic;

namespace SDIKit.Common.Identity
{
    public interface IWorkContext
    {
        bool IsAuthenticated { get; set; }
        CurrentUserContext CurrentUser { get; set; }
        List<long> CurrentUserRoleIds { get; set; }
    }
}