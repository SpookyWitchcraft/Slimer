using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Services.Interfaces;
using Slimer.Validators;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class ChatGptController_Tests
    {
        private readonly IChatGptService _serviceMock;

        public ChatGptController_Tests()
        {
            _serviceMock = CreateServiceMock();
        }

        [Fact]
        public void MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ChatGptController(new QueryParameterValidator(), null!));
        }

        [Fact]
        public void MissingValidatorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ChatGptController(null!, _serviceMock));
        }

        [Fact]
        public async Task Get_ShouldThrow_ForNull()
        {
            var controller = new ChatGptController(new QueryParameterValidator(), _serviceMock);

            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Get(default!));
        }

        [Fact]
        public async Task Get_ShouldThrow_ForEmpty()
        {
            var controller = new ChatGptController(new QueryParameterValidator(), _serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(string.Empty!));
        }

        [Fact]
        public async Task Get_ShouldThrow_ForLength()
        {
            var controller = new ChatGptController(new QueryParameterValidator(), _serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(new string('#', 256)));
        }

        [Fact]
        public async Task Get_ShouldReturnObjectAnd200()
        {
            var controller = new ChatGptController(new QueryParameterValidator(), _serviceMock);

            var results = await controller.Get("This is my question") as OkObjectResult;

            var response = results?.Value as string[];

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.True(response?.Length == 3);
        }

        private static IChatGptService CreateServiceMock()
        {
            var mock = new Mock<IChatGptService>();

            mock.Setup(x => x.AskQuestionAsync(It.IsAny<string>())).ReturnsAsync(new[] { "Hello", "I am", "a response." });

            return mock.Object;
        }
    }
}
