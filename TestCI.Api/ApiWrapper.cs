using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestCI.Api.Models;

namespace TestCI.Api
{
    internal class ApiWrapper : IApiWrapper
    {
        private readonly HttpClient _httpClient;

        public ApiWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync()
        {
            using var response = await _httpClient.GetAsync("/test");
            var text = await response.Content.ReadAsStringAsync();
            return text;
        }

        public async Task<SampleObject?> GetObjectAsync()
        {
            using var response = await _httpClient.GetAsync("/test/object");
            var sampleObject = await response.Content.ReadFromJsonAsync<SampleObject>();
            return sampleObject;
        }
    }
}
