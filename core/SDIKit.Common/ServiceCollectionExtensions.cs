using SDIKit.Common.Configurations;
using SDIKit.Common.Identity;
using SDIKit.Common.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SDIKit.Common
{
    public static class ServiceCollectionExtensions
    {
        #region [Add WorkContext]

        public static void AddSDIKitWorkContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped<IWorkContext, WebWorkContext>();
        }

        /// <summary>
        /// WorkContext Middleware sets the user information set in the request to IWorkContext properties.
        /// </summary>
        /// <typeparam name="TKey">Entity key type parameter</typeparam>
        /// <param name="app"></param>
        public static void UseSDIKitWorkContext(this IApplicationBuilder app)
        {
            app.UseMiddleware<WorkContextMiddleware>();
        }

        #endregion [Add WorkContext]

        #region [Add Settings]

        public static void AddSDIKitSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
        }

        #endregion [Add Settings]
    }
}