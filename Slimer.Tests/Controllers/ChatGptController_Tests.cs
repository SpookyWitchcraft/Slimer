using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Services.Interfaces;
using System;
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
        public void ChatGptController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ChatGptController(null!));
        }

        [Fact]
        public async void ChatGptController_PostShouldReturnObjectAnd200()
        {
            var controller = new ChatGptController(_serviceMock);

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
