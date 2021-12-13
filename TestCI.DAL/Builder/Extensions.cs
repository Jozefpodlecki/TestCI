using Microsoft.Extensions.DependencyInjection;
using TestCI.DAL.Repositories;

namespace TestCI.DAL.Builder
{
    public static class Extensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
