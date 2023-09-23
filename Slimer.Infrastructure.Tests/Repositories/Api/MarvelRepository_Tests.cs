using Moq;
using Slimer.Domain.Contracts.Marvel;
using Slimer.Infrastructure.Repositories.Api;
using Slimer.Infrastructure.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Infrastructure.Tests.Repositories.Api
{
    public class MarvelRepository_Tests
    {
        private readonly IHttpClientService _clientMock;
        private readonly ISecretsService _secretsMock;

        public MarvelRepository_Tests()
        {
            _clientMock = CreateHttpClinetProxyMock();
            _secretsMock = CreateSecretServiceMock();
        }

        [Fact]
        public void MarvelRepository_MissingClientShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new MarvelRepository(null!, _secretsMock));
        }

        [Fact]
        public void MarvelRepository_MissingSecretServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new MarvelRepository(_clientMock, null!));
        }

        [Fact]
        public async Task GetCharacterDetailsAsync_ReturnsObject()
        {
            var repo = new MarvelRepository(_clientMock, _secretsMock);
            var characterResponse = CreateMarvelResponseMessage().Data.Results[0];

            var results = await repo.GetCharacterDetailsAsync("Juggernaut");

            Assert.NotNull(results);
            Assert.Equal(characterResponse.Id, results.Id);
            Assert.Equal(characterResponse.Name, results.Name);
            Assert.Equal(characterResponse.Description, results.Description);
        }

        private static IHttpClientService CreateHttpClinetProxyMock()
        {
            var mock = new Mock<IHttpClientService>();

            mock.Setup(x => x.GetAsync<MarvelCharacterResponse>(It.IsAny<string>()))
                .ReturnsAsync(CreateMarvelResponseMessage());

            return mock.Object;
        }

        private static MarvelCharacterResponse CreateMarvelResponseMessage()
        {
            return new MarvelCharacterResponse
            {
                Data = new MarvelCharacterData
                {
                    Results = new MarvelDataResults[1]
                    {
                        CreateMarvelDataResults()
                    }
                }
            };
        }

        private static MarvelDataResults CreateMarvelDataResults()
        {
            return new MarvelDataResults
            {
                Description = "Description",
                Id = "1",
                Name = "Juggernaut"
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
