using Discord.Commands;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestCI.Modules;
using System;
using System.Threading.Tasks;
using TestCI.Queue;
using Microsoft.Extensions.Logging.Abstractions;
using TestCI.Api;
using TestCI.Queue.Messages;

namespace TestCI.Modules.Tests
{
    [TestClass]
    public class SampleMessageHandlerTests
    {
        private readonly SampleMessageHandler _messageHandler;
        private readonly Mock<IApiWrapper> _apiWrapperMock = new(MockBehavior.Strict);
        private readonly IServiceProvider _serviceProvider;

        public SampleMessageHandlerTests()
        {
            var serviceCollection = new ServiceCollection();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _messageHandler = new SampleMessageHandler(
                NullLogger<SampleMessageHandler>.Instance,
                _apiWrapperMock.Object,
                _serviceProvider);
        }

        [TestMethod]
        public async Task Should_Parse_Values()
        {
            var message = new SampleMessage();

            _apiWrapperMock
                .Setup(pr => pr.GetAsync())
                .ReturnsAsync("test");

            await _messageHandler.HandleAsync(message);
        }
    }
}
