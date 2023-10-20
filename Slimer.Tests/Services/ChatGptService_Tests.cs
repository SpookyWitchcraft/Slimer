using Moq;
using Slimer.Domain.Contracts.ChatGpt;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Services
{
    public class ChatGptService_Tests
    {
        private readonly IChatGptRepository _repositoryMock;

        public ChatGptService_Tests()
        {
            _repositoryMock = CreateRepositoryMock();
        }

        [Fact]
        public void ChatGptService_MissingRepositoryShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ChatGptService(null!));
        }

        [Fact]
        public async Task CreateIssueAsync_ReturnsObject()
        {
            var service = new ChatGptService(_repositoryMock);

            var results = (await service.AskQuestionAsync("what time is it?")).Lines.ToList();

            Assert.NotNull(results);
            Assert.True(results.Count == 1);
            Assert.Equal("10pm", results[0]);
        }

        private static IChatGptRepository CreateRepositoryMock()
        {
            var mock = new Mock<IChatGptRepository>();

            mock.Setup(x => x.GetAnswerAsync(It.IsAny<string>()))
                .ReturnsAsync(_chatFake);

            return mock.Object;
        }

        private static readonly GptTextResponse _chatFake = new GptTextResponse { Lines = new[] { "10pm" } };
    }
}
