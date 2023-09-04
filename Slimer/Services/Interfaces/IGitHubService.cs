using Slimer.Domain.Contracts.GitHub;

namespace Slimer.Services.Interfaces
{
    public interface IGitHubService
    {
        Task<GitHubResponse> CreateIssueAsync(GitHubRequest request);
    }
}
