using Moq;
using Slimer.Domain.Models.Trivia;
using Slimer.Infrastructure.Modules.Sql.Interfaces;
using Slimer.Infrastructure.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Infrastructure.Tests.Repositories.Sql
{
    public class TriviaQuestionRepository_Tests
    {
        private readonly ISqlExecutor _executorMock;

        public TriviaQuestionRepository_Tests()
        {
            _executorMock = CreateExecutorMock();
        }

        [Fact]
        public void TriviaQuestionRepository_MissingExecutorShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TriviaQuestionRepository(null!));
        }

        [Fact]
        public async Task GetTriviaQuestionByIdAsync_ShouldReturnTriviaQuestion()
        {
            var repo = new TriviaQuestionRepository(_executorMock);
            var question = CreateTriviaQuestion();

            var results = await repo.GetTriviaQuestionByIdAsync(1);

            Assert.NotNull(results);
            Assert.Equal(question.Id, results.Id);
            Assert.Equal(question.Question, results.Question);
            Assert.Equal(question.Answer, results.Answer);
            Assert.Equal(question.Category, results.Category);
            Assert.Equal(question.IsEnabled, results.IsEnabled);
            Assert.True(results.CreatedDate != default);
            Assert.True(results.UpdatedDate != default);
        }

        [Fact]
        public async Task GetTriviaQuestionByIdAsync_ShouldReturnDefault()
        {
            var repo = new TriviaQuestionRepository(CreateFailureMock());

            var results = await repo.GetTriviaQuestionByIdAsync(1);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetTriviaQuestionsAsync_ShouldReturnTriviaQuestions()
        {
            var repo = new TriviaQuestionRepository(_executorMock);
            var question = CreateTriviaQuestion();

            var results = (await repo.GetQuestionsAsync()).ToList();

            Assert.NotNull(results);
            Assert.True(results.Count == 3);
            Assert.Equal(question.Id, results[0].Id);
            Assert.Equal(question.Question, results[0].Question);
            Assert.Equal(question.Answer, results[0].Answer);
            Assert.Equal(question.Category, results[0].Category);
            Assert.Equal(question.IsEnabled, results[0].IsEnabled);
            Assert.True(results[0].CreatedDate != default);
            Assert.True(results[0].UpdatedDate != default);
        }

        [Fact]
        public async Task GetTriviaQuestionsAsync_ShouldReturnDefault()
        {
            var repo = new TriviaQuestionRepository(CreateFailureMock());

            var results = await repo.GetQuestionsAsync();

            Assert.Null(results);
        }

        [Fact]
        public async Task SaveAsync_ShouldReturnTriviaQuestion()
        {
            var repo = new TriviaQuestionRepository(_executorMock);
            var question = CreateTriviaQuestion();

            var results = await repo.SaveAsync(question);

            Assert.NotNull(results);
            Assert.Equal(question.Id, results.Id);
            Assert.Equal(question.Question, results.Question);
            Assert.Equal(question.Answer, results.Answer);
            Assert.Equal(question.Category, results.Category);
            Assert.Equal(question.IsEnabled, results.IsEnabled);
            Assert.True(results.CreatedDate != default);
            Assert.True(results.UpdatedDate != default);
        }

        [Fact]
        public async Task SaveAsync_ShouldReturnDefault()
        {
            var repo = new TriviaQuestionRepository(CreateFailureMock());

            var results = await repo.SaveAsync(CreateTriviaQuestion());

            Assert.Null(results);
        }

        private static ISqlExecutor CreateExecutorMock()
        {
            var mock = new Mock<ISqlExecutor>();

            mock.Setup(x => x.Read(It.IsAny<Func<IDataReader, TriviaQuestion>>(), It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .ReturnsAsync(CreateTriviaQuestion());

            mock.Setup(x => x.ReadList(It.IsAny<Func<IDataReader, TriviaQuestion>>(), It.IsAny<string>(), default!))
                .ReturnsAsync(new List<TriviaQuestion> { CreateTriviaQuestion(), CreateTriviaQuestion(), CreateTriviaQuestion() });

            mock.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .ReturnsAsync(true);

            return mock.Object;
        }

        private static ISqlExecutor CreateFailureMock()
        {
            var mock = new Mock<ISqlExecutor>();

            mock.Setup(x => x.Read(It.IsAny<Func<IDataReader, TriviaQuestion>>(), It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .ThrowsAsync(new Exception());

            mock.Setup(x => x.ReadList(It.IsAny<Func<IDataReader, TriviaQuestion>>(), It.IsAny<string>(), default!))
               .ThrowsAsync(new Exception());

            mock.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .ThrowsAsync(new Exception());

            return mock.Object;
        }

        private static TriviaQuestion CreateTriviaQuestion()
        {
            return new TriviaQuestion(
                1,
                "question",
                "answer",
                "category",
                true,
                DateTime.Now,
                DateTime.Now
                );
        }
    }
}
