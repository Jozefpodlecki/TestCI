using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TestCI.Queue.Messages;

namespace TestCI.Queue
{
    public static class Extensions
    {
        private static readonly Type _messageHandlerType = typeof(IMessageHandler<>);
        private static IDictionary<Type, Type> _messageHandlerTypeCache = new Dictionary<Type, Type>();

        public static IMessageHandler GetMessageHandler<T>(this IServiceProvider serviceProvider, T message)
           where T : BaseMessage
        {
            var messageType = message.GetType();

            if (!_messageHandlerTypeCache.TryGetValue(messageType, out var messageHandlerType))
            {
                messageHandlerType = _messageHandlerType.MakeGenericType(messageType);
                _messageHandlerTypeCache[messageType] = messageHandlerType;
            }

            return (IMessageHandler)serviceProvider.GetRequiredService(messageHandlerType);
        }
    }
}
