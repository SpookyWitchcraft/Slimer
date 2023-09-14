using Moq;
using Slimer.Domain.Contracts.GitHub;
using Slimer.Infrastructure.Repositories.Api;
using Slimer.Infrastructure.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Infrastructure.Tests.Repositories.Api
{
    public class GitHubRepository_Tests
    {
        private readonly IHttpClientService _clientMock;
        private readonly ISecretsService _secretsMock;

        public GitHubRepository_Tests()
        {
            _clientMock = CreateHttpClinetProxyMock();
            _secretsMock = CreateSecretServiceMock();
        }

        [Fact]
        public void GitHubRepository_MissingClientShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubRepository(null!, _secretsMock));
        }

        [Fact]
        public void GitHubRepository_MissingSecretServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubRepository(_clientMock, null!));
        }

        [Fact]
        public async Task CreateIssueAsync_ReturnsObject()
        {
            var repo = new GitHubRepository(_clientMock, _secretsMock);

            var results = await repo.PostIssueAsync(new GitHubRequest());

            Assert.NotNull(results);
        }

        private static IHttpClientService CreateHttpClinetProxyMock()
        {
            var mock = new Mock<IHttpClientService>();

            mock.Setup(x => x.SendAsync<GitHubResponse>(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new GitHubResponse());

            return mock.Object;
        }

        private static ISecretsService CreateSecretServiceMock()
        {
            var mock = new Mock<ISecretsService>();

            mock.Setup(x => x.GetValue(It.IsAny<string>())).Returns("secret value");

            return mock.Object;
        }
    }
}
