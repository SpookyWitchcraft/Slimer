﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Slimer.Controllers;
using Slimer.Domain.Models.Trivia;
using Slimer.Services.Interfaces;
using Slimer.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class TriviaController_Tests
    {
        private readonly ITriviaQuestionService _serviceMock;
        private readonly IdParameterValidator _idValidator;
        private readonly TriviaQuestionValidator _questionValidator;

        public TriviaController_Tests()
        {
            _serviceMock = CreateServiceMock();
            _idValidator = new IdParameterValidator();
            _questionValidator = new TriviaQuestionValidator();
        }

        [Fact]
        public void TriviaController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TriviaController(_questionValidator, _idValidator, null!));
        }

        [Fact]
        public void TriviaController_MissingIdValidatorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TriviaController(_questionValidator, null!, _serviceMock));
        }

        [Fact]
        public void TriviaController_MissingQuestionValidatorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TriviaController(null!, _idValidator, _serviceMock));
        }

        [Fact]
        public async Task Get_ShouldThrow_ForBadId()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(0));
        }

        [Fact]
        public async Task Search_ShouldThrow_ForBadId()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Search(0));
        }

        [Fact]
        public async Task Get_AnswerShouldThrow_ForNull()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Answer = null!;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_AnswerShouldThrow_ForEmpty()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Answer = string.Empty;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_AnswerShouldThrow_ForLength()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Answer = new string('#', 5001);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_QuestionShouldThrow_ForNull()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Question = null!;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_QuestionShouldThrow_ForEmpty()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Question = string.Empty;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_QuestionShouldThrow_ForLength()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Question = new string('#', 5001);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_CategoryShouldThrow_ForNull()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Category = null!;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_CategoryShouldThrow_ForEmpty()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Category = string.Empty;

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_CategoryShouldThrow_ForLength()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = CreateTriviaQuestion(1);

            request.Category = new string('#', 256);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_CreatedDateShouldThrow_ForDefault()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = new TriviaQuestion(1, "what?", "that", "general", true, default, DateTime.Now);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task Get_UpdatedDateShouldThrow_ForDefault()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            var request = new TriviaQuestion(1, "what?", "that", "general", true, DateTime.Now, default);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Post(request));
        }

        [Fact]
        public async Task TriviaController_GetShouldReturn200()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

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
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(0));
        }

        [Fact]
        public async Task TriviaController_GetWithIdShouldReturn200()
        {
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

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
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

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
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

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
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

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
            var controller = new TriviaController(_questionValidator, _idValidator, _serviceMock);

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
