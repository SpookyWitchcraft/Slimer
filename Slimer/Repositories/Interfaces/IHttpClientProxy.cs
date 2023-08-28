namespace Slimer.Repositories.Interfaces
{
    public interface IHttpClientProxy
    {
        Task<T> GetAsync<T>(string url);

        Task<T> SendAsync<T>(HttpRequestMessage content);
    }
}
