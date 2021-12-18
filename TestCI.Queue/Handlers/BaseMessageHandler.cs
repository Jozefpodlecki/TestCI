using System.Threading.Tasks;
using TestCI.Queue.Messages;

namespace TestCI.Queue
{
    internal abstract class BaseMessageHandler<T> : IMessageHandler<T>
        where T : BaseMessage
    {
        public Task HandleAsync(BaseMessage baseMessage) => HandleAsync((T)baseMessage);

        public abstract Task HandleAsync(T baseMessage);
    }
}
