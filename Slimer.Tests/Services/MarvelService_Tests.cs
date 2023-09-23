using Moq;
using Slimer.Domain.Contracts.Marvel;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Services
{
    public class MarvelService_Tests
    {
        private readonly IMarvelRepository _repositoryMock;

        public MarvelService_Tests()
        {
            _repositoryMock = CreateRepositoryMock();
        }

        [Fact]
        public void MarvelService_MissingRepositoryShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new MarvelService(null!));
        }

        [Fact]
        public async Task SearchForCharacterDetailsAsync_ReturnsObject()
        {
            var service = new MarvelService(_repositoryMock);

            var results = await service.SearchForCharacterDetailsAsync("Hero");

            Assert.NotNull(results);
            Assert.Equal("1", results.Id);
            Assert.Equal("Hero", results.Name);
            Assert.Equal("Real fun person.", results.Description);
        }

        [Fact]
        public async Task SearchForCharacterDetailsAsync_ReturnsNull()
        {
            var service = new MarvelService(_repositoryMock);

            var results = await service.SearchForCharacterDetailsAsync("hero");

            Assert.Null(results);
        }

        private static IMarvelRepository CreateRepositoryMock()
        {
            var mock = new Mock<IMarvelRepository>();

            mock.Setup(x => x.GetCharacterDetailsAsync(It.Is<string>(x => x == "Hero")))
                .ReturnsAsync(_marvelFake);

            return mock.Object;
        }

        private static readonly MarvelDataResults _marvelFake = new MarvelDataResults { Id = "1", Name = "Hero", Description = "Real fun person." };
    }
}
