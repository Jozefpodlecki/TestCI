using Discord.Commands;
using System.Threading.Tasks;
using TestCI.Queue;
using TestCI.Queue.Messages;
using TestCI.Services;

namespace TestCI.Modules
{
    public class AnotherModule : AppModuleBasee
    {
        private readonly IBlockingQueue _blockingQueue;

        public AnotherModule(IBlockingQueue blockingQueue)
        {
            _blockingQueue = blockingQueue;
        }

        public override void Dispose()
        {
            
        }

        [Command("queue")]
        [Alias("queue-item")]
        [Summary("does something")]
        [Remarks(""), SamplePrecondition]
        public async Task QueueItemAsync()
        {
            var enqueue = _blockingQueue.TryAdd(new SampleMessage
            {
            });

            if (enqueue)
            {
                await ReplyAsync("queued item");
            }

            await ReplyAsync("could not enqueue item");
        }
    }
}
