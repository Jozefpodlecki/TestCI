using System.Threading.Tasks;
using TestCI.Queue.Messages;

namespace TestCI.Queue
{
    public interface IMessageHandler
    {
        Task HandleAsync(BaseMessage message);
    }

    public interface IMessageHandler<in T> : IMessageHandler
        where T: BaseMessage
    {
        Task HandleAsync(T message);
    }
}
