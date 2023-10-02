using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Domain.Contracts.GitHub;
using Slimer.Services.Interfaces;
using Slimer.Validators;
using System;
using System.Threading.Tasks;
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
        public void MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubController(new GitHubRequestValidator(), null!));
        }

        [Fact]
        public void MissingValidatorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubController(null!, _serviceMock));
        }

        [Fact]
        public async Task Get_TitleShouldThrow_ForNull()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Title = null!;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_TitleShouldThrow_ForEmpty()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Title = string.Empty;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_TitleShouldThrow_ForLength()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Title = new string('#', 256);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_BodyShouldThrow_ForNull()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Body = null!;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_BodyShouldThrow_ForEmpty()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Body = string.Empty;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_BodyShouldThrow_ForLength()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Body = new string('#', 256);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_LabelsShouldThrow_ForNull()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Labels = null!;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_LabelsShouldThrow_ForEmpty()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Labels = new string[0];

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_LabelsShouldThrow_ForNullItem()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Labels = new string[1] { null! };

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_LabelsShouldThrow_ForEmptyItem()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Labels = new string[1] { string.Empty };

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_LabelsShouldThrow_ForMissingTag()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

            request.Labels = new string[1] { "Feature" };

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async void Post_ShouldReturnObjectAnd200()
        {
            var controller = new GitHubController(new GitHubRequestValidator(), _serviceMock);

            var request = CreateRequest();

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

        private static GitHubRequest CreateRequest()
        {
            return new GitHubRequest
            {
                Body = "body",
                Labels = new string[] { "bug" },
                Title = "title"
            };
        }
    }
}
