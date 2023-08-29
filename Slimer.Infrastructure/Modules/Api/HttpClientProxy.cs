using Newtonsoft.Json;
using Slimer.Infrastructure.Modules.Api.Interfaces;

namespace Slimer.Infrastructure.Modules.Api
{
    public class HttpClientProxy : IHttpClientProxy
    {
        private readonly HttpClient _httpClient;

        public HttpClientProxy(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            var results = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(results)!;
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage content)
        {
            var response = await _httpClient.SendAsync(content);

            var results = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(results)!;
        }
    }
}
