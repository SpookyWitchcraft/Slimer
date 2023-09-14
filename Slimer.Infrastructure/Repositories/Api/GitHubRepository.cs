using Slimer.Domain.Contracts.GitHub;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Infrastructure.Services.Interfaces;

namespace Slimer.Infrastructure.Repositories.Api
{
    public class GitHubRepository : IGitHubRepository
    {
        private readonly IHttpClientService _client;
        private readonly ISecretsService _secretsService;

        public GitHubRepository(IHttpClientService client, ISecretsService secretsService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _secretsService = secretsService ?? throw new ArgumentNullException(nameof(secretsService));
        }

        public async Task<GitHubResponse> PostIssueAsync(GitHubRequest request)
        {
            var headers = new List<(string, string)> { ("Accept", "application/vnd.github+json"), ("X-GitHub-Api-Version", "2022-11-28"), ("User-Agent", "Slimer") };

            var content = _client.CreateStringContent(request);
            var httpRequest = _client.CreateBearerRequest(content, HttpMethod.Post, _secretsService.GetValue("GitHubToken"), "https://api.github.com/repos/SpookyWitchcraft/BabaYaga/issues", headers);

            return await _client.SendAsync<GitHubResponse>(httpRequest);
        }
    }
}
