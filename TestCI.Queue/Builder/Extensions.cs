﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TestCI.Queue;
using TestCI.Queue.Messages;

namespace TestCI.Queue.Builder
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        private static readonly Type _messageHandlerType = typeof(IMessageHandler<>);
        private static IDictionary<Type, Type> _messageHandlerTypeCache = new Dictionary<Type, Type>();

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
