using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCI.Common;
using TestCI.HostedService;
using TestCI.Queue;
using TestCI.Queue.Messages;
using TestCI.Services;

namespace TestCI.HostedService.Tests
{
    [TestClass]
    public class MessageHandlingHostedServiceTests
    {
        private readonly MessageHandlingHostedService _messageHandlingHostedService;
        private readonly Mock<IBlockingQueue> _blockingQueueMock = new(MockBehavior.Strict);
        private readonly Mock<IMessageHandler<SampleMessage>> _messageHandlerMock = new(MockBehavior.Strict);

        public MessageHandlingHostedServiceTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped((sp) => _messageHandlerMock.Object);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            _messageHandlingHostedService = new MessageHandlingHostedService(
                NullLogger<MessageHandlingHostedService>.Instance,
                _blockingQueueMock.Object,
                serviceScopeFactory);
        }

        public async Task Should_Process_Message()
        {
            var cancellationToken = new CancellationToken();
            var message = new SampleMessage();
            var messages = new[]
            {
                message,
            };

            _blockingQueueMock
                .Setup(pr => pr.GetEnumerable(cancellationToken))
                .Returns(messages);

            _messageHandlerMock
                .Setup(pr => pr.HandleAsync(message))
                .Returns(Task.CompletedTask);

            await _messageHandlingHostedService.StartAsync(cancellationToken);
        }
    }
}
