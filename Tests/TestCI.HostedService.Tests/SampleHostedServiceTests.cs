using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCI.Common;
using TestCI.HostedService;
using TestCI.Services;

namespace TestCI.HostedService.Tests
{
    [TestClass]
    public class SampleHostedServiceTests
    {
        private readonly SampleHostedService _sampleHostedService;
        private readonly Mock<ISampleService> _sampleServiceMock = new(MockBehavior.Strict);
        private readonly Mock<ITimer> _timerMock = new(MockBehavior.Strict);
        private readonly Mock<ITaskManager> _taskManagerMock = new(MockBehavior.Strict);

        public SampleHostedServiceTests()
        {
            _sampleHostedService = new SampleHostedService(
                _sampleServiceMock.Object,
                _timerMock.Object,
                _taskManagerMock.Object);
        }

        public async Task SetupAsync()
        {
            _timerMock
                .Setup(pr => pr.Start(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()));

            _taskManagerMock
                .Setup(pr => pr.Delay(It.IsAny<TimeSpan>()))
                .Returns(Task.CompletedTask);

            var cancellationToken = new CancellationToken();
            await _sampleHostedService.StartAsync(cancellationToken);
        }

        [TestMethod]
        public async Task Should_Run_OnTick()
        {
            await SetupAsync();

            _sampleServiceMock
                .Setup(pr => pr.ShouldExit())
                .ReturnsAsync(true);

            _timerMock.Raise(pr => pr.Tick += null, null, null);
        }

        [TestMethod]
        public async Task Should_Throw_OnTick()
        {
            await SetupAsync();

            _sampleServiceMock
                .Setup(pr => pr.ShouldExit())
                .ReturnsAsync(false);

            _timerMock.Raise(pr => pr.Tick += null, null, null);

        }
    }
}
