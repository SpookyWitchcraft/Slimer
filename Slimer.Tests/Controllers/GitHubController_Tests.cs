using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Domain.Contracts.GitHub;
using Slimer.Services.Interfaces;
using Slimer.Validators;
using System;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class GitHubController_Tests
    {
        private readonly IGitHubService _serviceMock;

        public GitHubController_Tests()
        {
            _serviceMock = CreateServiceMock();
        }

        [Fact]
        public void GitHubController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubController(new GitHubRequestValidator(), null!));
        }

        [Fact]
        public void GitHubController_MissingValidatorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubController(null!, _serviceMock));
        }

        [Fact]
        public async void GitHubController_PostShouldReturnObjectAnd200()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = new GitHubRequest
            {
                Body = "body",
                Labels = new string[] { "bug" },
                Title = "title"
            };

            var results = await controller.Post(request) as OkObjectResult;

            var response = results?.Value as GitHubResponse;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.Equal("http://www.wwwdotcom.com", response.HtmlUrl);
        }

        private static IGitHubService CreateServiceMock()
        {
            var mock = new Mock<IGitHubService>();

            mock.Setup(x => x.CreateIssueAsync(It.IsAny<GitHubRequest>())).ReturnsAsync(new GitHubResponse
            {
                HtmlUrl = "http://www.wwwdotcom.com"
            });

            return mock.Object;
        }
    }
}
