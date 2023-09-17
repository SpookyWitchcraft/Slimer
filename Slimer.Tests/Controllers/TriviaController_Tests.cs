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
        public async Task TriviaController_GetShouldThrowBadRequest()
        {
            var controller = new TriviaController(_serviceMock);

            await Assert.ThrowsAsync<BadHttpRequestException>(() => controller.Get(0));
        }

        private static ITriviaQuestionService CreateServiceMock()
        {
            var mock = new Mock<ITriviaQuestionService>();

            mock.Setup(x => x.GetRandomQuestionAsync())
                .ReturnsAsync(new TriviaQuestion(1, "", "", "", true, DateTime.Now, DateTime.Now));

            mock.Setup(x => x.GetQuestionByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new TriviaQuestion(1, "", "", "", true, DateTime.Now, DateTime.Now));

            mock.Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync(new Dictionary<int, TriviaQuestion>
                {
                    { 1, new TriviaQuestion(1, "", "", "", true, DateTime.Now, DateTime.Now) }
                });

            mock.Setup(x => x.SaveAsync(It.IsAny<TriviaQuestion>()))
                .ReturnsAsync(new TriviaQuestion(1, "", "", "", true, DateTime.Now, DateTime.Now));

            mock.Setup(x => x.InvalidateCache())
                .Returns(true);

            return mock.Object;
        }

        private static ITriviaQuestionService CreateFailureMock()
        {
            var mock = new Mock<ITriviaQuestionService>();

            mock.Setup(x => x.GetRandomQuestionAsync())
                .ThrowsAsync(new Exception());

            mock.Setup(x => x.GetQuestionByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            mock.Setup(x => x.GetQuestionsAsync())
                .ThrowsAsync(new Exception());

            mock.Setup(x => x.SaveAsync(It.IsAny<TriviaQuestion>()))
                .ThrowsAsync(new Exception());

            mock.Setup(x => x.InvalidateCache())
                .Throws(new Exception());

            return mock.Object;
        }
    }
}
