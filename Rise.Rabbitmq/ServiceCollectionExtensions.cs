using Microsoft.Extensions.DependencyInjection;

namespace Rise.Rabbitmq
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitmq(this IServiceCollection services)
        {
            services.AddScoped<IRabbitmqPost, RabbitmqPost>();
            return services;
        }
    }
}
