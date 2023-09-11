using Newtonsoft.Json;
using Slimer.Domain.Contracts.GitHub;
using Slimer.Infrastructure.Modules.Api.Interfaces;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Infrastructure.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace Slimer.Infrastructure.Repositories.Api
{
    public class GitHubRepository : IGitHubRepository
    {
        private readonly IHttpClientProxy _client;
        private readonly ISecretsService _secretsService;

        public GitHubRepository(IHttpClientProxy client, ISecretsService secretsService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _secretsService = secretsService ?? throw new ArgumentNullException(nameof(secretsService));
        }

        public async Task<GitHubResponse> CreateIssueAsync(GitHubRequest request)
        {
            var httpRequest = CreateRequest(CreateContent(request), "https://api.github.com/repos/SpookyWitchcraft/BabaYaga/issues");

            return await _client.SendAsync<GitHubResponse>(httpRequest);
        }

        private StringContent CreateContent(GitHubRequest request)
        {
            var serialized = JsonConvert.SerializeObject(request);

            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        private HttpRequestMessage CreateRequest(HttpContent content, string url)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri(url),
            };

            request.Headers.Add("Accept", "application/vnd.github+json");
            request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
            request.Headers.Add("User-Agent", "Slimer");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _secretsService.GetValue("GitHubToken"));

            return request;
        }
    }
}
