using System.Threading.Tasks;

namespace TestCI.Services
{
    public interface ISampleService
    {
        Task<bool> ShouldExit();

        Task<string> GetMessageAsync();
    }
}
