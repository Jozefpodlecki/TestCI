using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using TestCI.Queue.Messages;

namespace TestCI.Queue
{
    internal class BlockingQueue : IBlockingQueue
    {
        private readonly BlockingCollection<BaseMessage> _blockingCollection;

        public BlockingQueue()
        {
            _blockingCollection = new BlockingCollection<BaseMessage>();
        }

        public IEnumerable<BaseMessage> GetEnumerable(CancellationToken cancellationToken = default) 
            => _blockingCollection.GetConsumingEnumerable(cancellationToken);

        public bool TryAdd(BaseMessage message) => _blockingCollection.TryAdd(message);
    }
}
