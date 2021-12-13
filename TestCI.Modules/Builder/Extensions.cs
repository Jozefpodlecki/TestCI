using Microsoft.Extensions.DependencyInjection;

namespace TestCI.Modules.Builder
{
    public static class Extensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services)
        {
            return services;
        }
    }
}
