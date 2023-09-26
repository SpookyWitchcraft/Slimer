using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Domain.Contracts.Marvel;
using Slimer.Services.Interfaces;
using Slimer.Validators;
using System;
using System.Threading.Tasks;
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
        public void MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new MarvelController(new QueryParameterValidator(), null!));
        }

        [Fact]
        public void MissingValidatorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new MarvelController(null!, _serviceMock));
        }

        [Fact]
        public async Task Get_ShouldThrow_ForNull()
        {
            var controller = new MarvelController(new QueryParameterValidator(), _serviceMock);

            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Get(default!));
        }

        [Fact]
        public async Task Get_ShouldThrow_ForEmpty()
        {
            var controller = new MarvelController(new QueryParameterValidator(), _serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(string.Empty!));
        }

        [Fact]
        public async Task Get_ShouldThrow_ForLength()
        {
            var controller = new MarvelController(new QueryParameterValidator(), _serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(new string('#', 256)));
        }

        [Fact]
        public async void Post_ShouldReturnObjectAnd200()
        {
            var controller = new MarvelController(new QueryParameterValidator(), _serviceMock);

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
