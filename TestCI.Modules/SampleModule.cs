using Discord.Commands;
using System.Threading.Tasks;
using TestCI.Services;

namespace TestCI.Modules
{
    public class SampleModule : AppModuleBasee
    {
        private readonly ISampleService _sampleService;

        public SampleModule(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public override void Dispose()
        {
            
        }

        [Command("test1")]
        [Alias("test2")]
        [Summary("does something")]
        [Remarks(""), SamplePrecondition]
        public async Task RunCommandAsync()
        {
            var message = await _sampleService.GetMessageAsync();

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            await ReplyAsync(message);
        }
    }
}
