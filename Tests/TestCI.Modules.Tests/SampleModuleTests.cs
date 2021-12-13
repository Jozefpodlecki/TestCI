using Discord;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using TestCI.Services;

namespace TestCI.Modules.Tests
{
    [TestClass]
    public class SampleModuleTests : TestBase
    {
        protected readonly SampleModule _module;
        protected readonly Mock<ISampleService> _sampleServiceMock = new(MockBehavior.Strict);

        public SampleModuleTests()
        {
            _module = new(_sampleServiceMock.Object);
            Initialize(_module);
        }

        [TestMethod]
        public async Task Should_Return_On_Empty_Message()
        {
            _sampleServiceMock
                .Setup(pr => pr.GetMessageAsync())
                .ReturnsAsync(string.Empty);

            await _module.RunCommandAsync();
        }

        [TestMethod]
        public async Task Should_Reply_With_Message()
        {
            _sampleServiceMock
                .Setup(pr => pr.GetMessageAsync())
                .ReturnsAsync("test");

            _messageChannelMock
                .Setup(pr => pr.SendMessageAsync(
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<Embed>(),
                    It.IsAny<RequestOptions>(),
                    It.IsAny<AllowedMentions>(),
                    It.IsAny<MessageReference>()))
                .ReturnsAsync(_userMessageMock.Object);

            await _module.RunCommandAsync();
        }
    }
}
