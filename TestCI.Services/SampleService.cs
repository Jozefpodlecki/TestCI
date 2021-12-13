using System.Threading.Tasks;

namespace TestCI.Services
{
    internal class SampleService : ISampleService
    {
        public Task<string> GetMessageAsync()
        {
            return Task.FromResult("test");
        }

        public Task<bool> ShouldExit()
        {
            return Task.FromResult(true);
        }
    }
}
