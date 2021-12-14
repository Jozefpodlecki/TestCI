using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace TestCI.Modules.Builder
{
    public static class Extensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services)
        {
            services.AddSingleton((sp) =>
            {
                var commandService = new CommandService();
                commandService.AddTypeReader<BoolTypeReader>(new BoolTypeReader());
                commandService.AddModulesAsync(typeof(AppModuleBasee).Assembly, sp);
                return commandService;
            });

            return services;
        }
    }
}
