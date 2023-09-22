using Moq;
using Slimer.Domain.Contracts.GitHub;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Services
{
    public class GitHubService_Tests
    {
        private readonly IGitHubRepository _repositoryMock;

        public GitHubService_Tests()
        {
            _repositoryMock = CreateRepositoryMock();
        }

        [Fact]
        public void GitHubService_MissingRepositoryShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubService(null!));
        }

        [Fact]
        public async Task CreateIssueAsync_ReturnsObject()
        {
            var service = new GitHubService(_repositoryMock);

            var results = await service.CreateIssueAsync(new() { Body = "", Labels = new[] { "bug" }, Title = "Bug!" });

            Assert.NotNull(results);
            Assert.Equal("http://github.com", results.HtmlUrl);
        }

        private static IGitHubRepository CreateRepositoryMock()
        {
            var mock = new Mock<IGitHubRepository>();

            mock.Setup(x => x.PostIssueAsync(It.IsAny<GitHubRequest>()))
                .ReturnsAsync(_gitHubFake);

            return mock.Object;
        }

        private static readonly GitHubResponse _gitHubFake = new() { HtmlUrl = "http://github.com" };
    }
}
