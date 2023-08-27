using Newtonsoft.Json;
using Slimer.Repositories.Interfaces;

namespace Slimer.Repositories
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
    }
}
