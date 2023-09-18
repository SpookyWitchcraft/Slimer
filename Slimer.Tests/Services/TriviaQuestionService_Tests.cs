using Moq;
using Slimer.Domain.Models.Trivia;
using Slimer.Infrastructure.Repositories.Sql.Interfaces;
using Slimer.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Services
{
    public class TriviaQuestionService_Tests
    {
        private readonly ITriviaQuestionRepository _repositoryMock;

        public TriviaQuestionService_Tests()
        {
            _repositoryMock = CreateRepositoryMock();
        }

        [Fact]
        public void TriviaQuestionService_MissingRepositoryShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TriviaQuestionService(null!));
        }

        [Fact]
        public async Task GetQuestionAsync_ReturnsObject()
        {
            var service = new TriviaQuestionService(_repositoryMock);

            var results = await service.GetRandomQuestionAsync();

            Assert.NotNull(results);
            Assert.Contains(results.Id, new [] { 0, 1, 2, 3 });
            Assert.True(results.IsEnabled);
        }

        [Fact]
        public async Task GetQuestionsAsync_ReturnsManyObjects()
        {
            var service = new TriviaQuestionService(_repositoryMock);

            var results = await service.GetQuestionsAsync();

            Assert.Equal(3, results.Count);
        }

        [Fact]
        public async Task SaveAsync_ShouldReturnSameObject()
        {
            var service = new TriviaQuestionService(_repositoryMock);

            var results = await service.SaveAsync(_triviaFake);

            Assert.Equal(_triviaFake.Id, results.Id);
        }

        [Fact]
        public void InvalidateCache_ShouldReturnTrue()
        {
            var service = new TriviaQuestionService(_repositoryMock);

            var results = service.InvalidateCache();

            Assert.True(results);
        }

        private static ITriviaQuestionRepository CreateRepositoryMock()
        {
            var mock = new Mock<ITriviaQuestionRepository>();

            mock.Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync(CreateQuestionsDictionary());

            mock.Setup(x => x.SaveAsync(_triviaFake))
                .ReturnsAsync(_triviaFake);

            return mock.Object;
        }

        private static List<TriviaQuestion> CreateQuestionsDictionary()
        {
            return new List<TriviaQuestion>
            {
                new TriviaQuestion(1, "a", "aa", "aaa", true, DateTime.Now, DateTime.Now),
                new TriviaQuestion(2, "b", "bb", "bbb", true, DateTime.Now, DateTime.Now),
                new TriviaQuestion(3, "c", "cc", "ccc", true, DateTime.Now, DateTime.Now)
            };
        }

        private static readonly TriviaQuestion _triviaFake = new(1, "a", "aa", "aaa", true, DateTime.Now, DateTime.Now);
    }
}
