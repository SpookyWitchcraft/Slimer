using Slimer.Domain.Contracts.GitHub;

namespace Slimer.Infrastructure.Repositories.Api.Interfaces
{
    public interface IGitHubRepository
    {
        Task<GitHubResponse> CreateIssueAsync(GitHubRequest request);
    }
}
