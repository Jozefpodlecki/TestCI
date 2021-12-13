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
        public async Task Should_Run_OnTick()
        {
            _sampleServiceMock
                .Setup(pr => pr.GetMessageAsync())
                .ReturnsAsync("test");

            await _module.RunCommandAsync();
        }
    }
}
