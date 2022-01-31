using SDIKit.Common.Identity;

using Microsoft.AspNetCore.Http;

using System;
using System.Threading.Tasks;

namespace SDIKit.Common.Middlewares
{
    public class WorkContextMiddleware
    {
        private readonly RequestDelegate next;

        public WorkContextMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context, IWorkContext workContext)
        {
            workContext.IsAuthenticated = context.User.Identity.IsAuthenticated;

            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                var currentUser = new CurrentUserContext
                {
                    Id = context.User.GetCurrentUserId(),
                    Email = context.User.GetEmail(),
                    Fullname = context.User.GetFullDisplayName(),
                    Name = context.User.GetFullDisplayName(),
                    Surname = context.User.GetSurname(),
                    LoginDate = context.User.GetLoginDate()
                };

                workContext.CurrentUser = currentUser;
            }

            await next(context);
        }
    }
}