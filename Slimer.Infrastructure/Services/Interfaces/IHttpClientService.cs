namespace Slimer.Infrastructure.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);

        Task<T> SendAsync<T>(HttpRequestMessage content);

        StringContent CreateStringContent(object obj);

        HttpRequestMessage CreateBearerRequest(HttpContent content, HttpMethod method, string token, string url, IEnumerable<(string key, string value)> headers = null);
    }
}
