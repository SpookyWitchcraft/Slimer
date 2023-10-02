using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Domain.Models.Trivia;
using Slimer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class TriviaController_Tests
    {
        private readonly ITriviaQuestionService _serviceMock;

        public TriviaController_Tests()
        {
            _serviceMock = CreateServiceMock();
        }

        [Fact]
        public void TriviaController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TriviaController(null!));
        }

        [Fact]
        public async Task TriviaController_GetShouldReturn200()
        {
            var controller = new TriviaController(_serviceMock);

            var results = await controller.Get() as OkObjectResult;

            var response = results?.Value as TriviaQuestion;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.Equal(1, response.Id);
            Assert.Equal("that", response.Answer);
            Assert.Equal("what?", response.Question);
            Assert.Equal("general", response.Category);
            Assert.True(response.IsEnabled);
            Assert.NotEqual(default, response.CreatedDate);
            Assert.NotEqual(default, response.UpdatedDate);
        }

        [Fact]
        public async Task TriviaController_GetWithIdShouldThrowBadRequest()
        {
            var controller = new TriviaController(_serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(0));
        }

        [Fact]
        public async Task TriviaController_GetWithIdShouldReturn200()
        {
            var controller = new TriviaController(_serviceMock);

            var results = await controller.Get(1) as OkObjectResult;

            var response = results?.Value as TriviaQuestion;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.Equal(1, response.Id);
            Assert.Equal("that", response.Answer);
            Assert.Equal("what?", response.Question);
            Assert.Equal("general", response.Category);
            Assert.True(response.IsEnabled);
            Assert.NotEqual(default, response.CreatedDate);
            Assert.NotEqual(default, response.UpdatedDate);
        }

        [Fact]
        public async Task TriviaController_SearchShouldReturn200()
        {
            var controller = new TriviaController(_serviceMock);

            var results = await controller.Search() as OkObjectResult;

            var response = results?.Value as IList<TriviaQuestion>;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.Equal(5, response.Count);
            Assert.Equal("that", response[0].Answer);
            Assert.Equal("what?", response[0].Question);
            Assert.Equal("general", response[0].Category);
            Assert.True(response[0].IsEnabled);
            Assert.NotEqual(default, response[0].CreatedDate);
            Assert.NotEqual(default, response[0].UpdatedDate);
        }

        [Fact]
        public async Task TriviaController_SearchWithIdShouldReturn200()
        {
            var controller = new TriviaController(_serviceMock);

            var results = await controller.Search(1) as OkObjectResult;

            var response = results?.Value as TriviaQuestion;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.Equal(1, response.Id);
            Assert.Equal("that", response.Answer);
            Assert.Equal("what?", response.Question);
            Assert.Equal("general", response.Category);
            Assert.True(response.IsEnabled);
            Assert.NotEqual(default, response.CreatedDate);
            Assert.NotEqual(default, response.UpdatedDate);
        }

        [Fact]
        public async Task TriviaController_PostShouldReturn200()
        {
            var controller = new TriviaController(_serviceMock);

            var question = CreateTriviaQuestion(1);

            var results = await controller.Post(question) as OkObjectResult;

            var response = results?.Value as TriviaQuestion;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.Equal(question.Id, response.Id);
            Assert.Equal(question.Answer, response.Answer);
            Assert.Equal(question.Question, response.Question);
            Assert.Equal(question.Category, response.Category);
            Assert.Equal(question.IsEnabled, response.IsEnabled);
            Assert.NotEqual(default, response.CreatedDate);
            Assert.NotEqual(default, response.UpdatedDate);
        }

        [Fact]
        public void TriviaController_InvalidateCacheShouldReturn200()
        {
            var controller = new TriviaController(_serviceMock);

            var results = controller.Invalidate() as OkObjectResult;

            var response = (bool)results?.Value;

            Assert.NotNull(results);
            Assert.NotNull(response);
            Assert.True(results?.StatusCode == 200);
            Assert.True(response);
        }

        private static ITriviaQuestionService CreateServiceMock()
        {
            var mock = new Mock<ITriviaQuestionService>();

            mock.Setup(x => x.GetRandomQuestionAsync())
                .ReturnsAsync(CreateTriviaQuestion(1));

            mock.Setup(x => x.GetQuestionByIdAsync(It.Is<int>(x => x == 1)))
                .ReturnsAsync(CreateTriviaQuestion(1));

            mock.Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync(new TriviaQuestion[]
                {
                    CreateTriviaQuestion(1),
                    CreateTriviaQuestion(2),
                    CreateTriviaQuestion(3),
                    CreateTriviaQuestion(4),
                    CreateTriviaQuestion(5)
                });

            mock.Setup(x => x.SaveAsync(It.IsAny<TriviaQuestion>()))
                .ReturnsAsync(CreateTriviaQuestion(1));

            mock.Setup(x => x.InvalidateCache())
                .Returns(true);

            return mock.Object;
        }

        private static TriviaQuestion CreateTriviaQuestion(int id)
        {
            return new TriviaQuestion(id, "what?", "that", "general", true, DateTime.Now, DateTime.Now);
        }
    }
}
