using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestCI.Services;

namespace TestCI.Modules
{
    public class SamplePrecondition : PreconditionAttribute
    {
        public override async Task<PreconditionResult> CheckPermissionsAsync(
            ICommandContext context,
            CommandInfo command,
            IServiceProvider services)
        {
            var sampleService = services.GetRequiredService<ISampleService>();

            if(await sampleService.ShouldExit())
            {
                return PreconditionResult.FromSuccess();
            }

            return PreconditionResult.FromError("error");
        }
    }
}