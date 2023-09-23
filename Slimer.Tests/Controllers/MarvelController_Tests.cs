using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Domain.Contracts.Marvel;
using Slimer.Services.Interfaces;
using System;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class MarvelController_Tests
    {
        private readonly IMarvelService _serviceMock;

        public MarvelController_Tests()
        {
            _serviceMock = CreateServiceMock();
        }

        [Fact]
        public void MarvelController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new MarvelController(null!));
        }

        [Fact]
        public async void MarvelController_PostShouldReturnObjectAnd200()
        {
            var controller = new MarvelController(_serviceMock);

            var results = await controller.Get("Juggernaut") as OkObjectResult;

            var response = results?.Value as MarvelDataResults;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.Equal("Unstoppable", response.Description);
            Assert.Equal("123", response.Id);
            Assert.Equal("Juggernaut", response.Name);
        }

        private static IMarvelService CreateServiceMock()
        {
            var mock = new Mock<IMarvelService>();

            mock.Setup(x => x.SearchForCharacterDetailsAsync(It.IsAny<string>())).ReturnsAsync(new MarvelDataResults
            {
                Description = "Unstoppable",
                Id = "123",
                Name = "Juggernaut"
            });

            return mock.Object;
        }
    }
}
