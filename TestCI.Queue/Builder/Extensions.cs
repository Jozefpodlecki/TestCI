using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TestCI.Queue;
using TestCI.Queue.Messages;

namespace TestCI.Queue.Builder
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        private static readonly Type _messageHandlerType = typeof(IMessageHandler<>);
        private static IDictionary<Type, Type> _messageHandlerTypeCache = new Dictionary<Type, Type>();

        public static async Task Test(this IServiceProvider serviceProvider, BaseMessage message)
        {
            IMessageHandler messageHandler;

            switch (message)
            {
                case SampleMessage sm:
                    messageHandler = serviceProvider.GetRequiredService<IMessageHandler<SampleMessage>>();
                    break;
                case AnotherMessage am:
                    messageHandler = serviceProvider.GetRequiredService<IMessageHandler<AnotherMessage>>();
                    break;
                default:
                    throw new Exception();
            }

            await messageHandler.HandleAsync(message);
        }

        public static IServiceCollection AddQueue(this IServiceCollection services)
        {
            services.AddSingleton<IBlockingQueue, BlockingQueue>();
            services.AddScoped<IMessageHandler<SampleMessage>, SampleMessageHandler>();
            services.AddScoped<IMessageHandler<AnotherMessage>, AnotherMessageHandler>();

            return services;
        }

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
