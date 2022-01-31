using System.Collections.Generic;

namespace SDIKit.Common.Identity
{
    public class WebWorkContext : IWorkContext
    {
        public WebWorkContext()
        {
            IsAuthenticated = false;
            CurrentUserRoleIds = new List<long>();
        }

        public bool IsAuthenticated { get; set; }
        public CurrentUserContext CurrentUser { get; set; }
        public List<long> CurrentUserRoleIds { get; set; }
    }
}