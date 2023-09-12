using Slimer.Domain.Contracts.GitHub;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Services.Interfaces;

namespace Slimer.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubRepository _repository;

        public GitHubService(IGitHubRepository repository)
        {
            _repository = repository;
        }

        public async Task<GitHubResponse> CreateIssueAsync(GitHubRequest request)
        {
            return await _repository.PostIssueAsync(request);
        }
    }
}
