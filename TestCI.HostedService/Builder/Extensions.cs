using Microsoft.Extensions.DependencyInjection;

namespace TestCI.HostedService.Builder
{
    public static class Extensions
    {
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<SampleHostedService>();

            return services;
        }
    }
}
