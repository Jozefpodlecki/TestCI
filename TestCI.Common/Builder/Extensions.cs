using Microsoft.Extensions.DependencyInjection;

namespace TestCI.Common.Builder
{
    public static class Extensions
    {
        public static IServiceCollection AddTimer(this IServiceCollection services)
        {
            services.AddSingleton<ITimer, ThreadingTimer>();
            return services;
        }

        public static IServiceCollection AddTaskManager(this IServiceCollection services)
        {
            services.AddSingleton<ITaskManager, TaskManager>();
            return services;
        }
    }
}
