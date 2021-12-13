using Discord;
using Discord.Commands;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestCI.Services;

namespace TestCI.Modules.Tests
{
    [TestClass]
    public class SamplePreconditionTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SamplePrecondition _preconditionAttribute;
        private readonly Mock<ICommandContext> _commandContextMock = new(MockBehavior.Strict);
        private readonly Mock<ISampleService> _sampleServiceMock = new(MockBehavior.Strict);

        public SamplePreconditionTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(_sampleServiceMock.Object);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _preconditionAttribute = new();
        }

        [TestMethod]
        public async Task Should_Return_Success()
        {
            _sampleServiceMock
                .Setup(pr => pr.ShouldExit())
                .ReturnsAsync(true);

            var result = await _preconditionAttribute.CheckPermissionsAsync(_commandContextMock.Object, null, _serviceProvider);
            result.IsSuccess.Should().BeTrue();
        }
    }
}
