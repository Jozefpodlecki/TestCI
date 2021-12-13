using Microsoft.Extensions.DependencyInjection;

namespace TestCI.Services.Builder
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ISampleService, SampleService>();

            return services;
        }
    }
}
