using Moq;
using Slimer.Domain.Contracts.ChatGpt;
using Slimer.Infrastructure.Repositories.Api;
using Slimer.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Infrastructure.Tests.Repositories.Api
{
    public class ChatGptRepository_Tests
    {
        private readonly IHttpClientService _clientMock;
        private readonly ISecretsService _secretsMock;

        public ChatGptRepository_Tests()
        {
            _clientMock = CreateHttpClinetProxyMock();
            _secretsMock = CreateSecretServiceMock();
        }

        [Fact]
        public void ChatGptRepository_MissingClientShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ChatGptRepository(null!, _secretsMock));
        }

        [Fact]
        public void ChatGptRepository_MissingSecretServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ChatGptRepository(_clientMock, null!));
        }

        [Fact]
        public async Task GetAnswerAsync_ReturnsObject()
        {
            var repo = new ChatGptRepository(_clientMock, _secretsMock);

            var results = await repo.GetAnswerAsync("teststring");

            Assert.NotNull(results);
        }

        private static IHttpClientService CreateHttpClinetProxyMock()
        {
            var mock = new Mock<IHttpClientService>();

            mock.Setup(x => x.SendAsync<GptResponse>(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(CreateChatGptResponseMessage());

            return mock.Object;
        }

        private static GptResponse CreateChatGptResponseMessage()
        {
            return new GptResponse
            {
                Choices = new List<GptChoice>
                {
                    new GptChoice
                    {
                        Message = new GptResponseMessage
                        {
                            Content = "A praying mantis lurked in the foliage, patiently awaiting its unsuspecting prey to venture too close. Ladybugs, those tiny red sentinels of the garden, patrolled the leaves, ready to pounce on any aphid that dared to feast on their precious plants."
                        }
                    }
                }
            };
        }

        private static ISecretsService CreateSecretServiceMock()
        {
            var mock = new Mock<ISecretsService>();

            mock.Setup(x => x.GetValue(It.IsAny<string>())).Returns("secret value");

            return mock.Object;
        }
    }
}
