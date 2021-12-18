using System.Collections.Generic;
using System.Threading;
using TestCI.Queue.Messages;

namespace TestCI.Queue
{
    public interface IBlockingQueue
    {
        bool TryAdd(BaseMessage message);

        IEnumerable<BaseMessage> GetEnumerable(CancellationToken cancellationToken = default);
    }
}
