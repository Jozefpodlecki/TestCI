using System.Threading.Tasks;
using TestCI.Api.Models;

namespace TestCI.Api
{
    public interface IApiWrapper
    {
        Task<string> GetAsync();

        Task<SampleObject?> GetObjectAsync();
    }
}
