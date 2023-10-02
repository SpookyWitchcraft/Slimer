using Slimer.Domain.Models.Trivia;
using Slimer.Infrastructure.Repositories.Sql.Interfaces;
using Slimer.Services.Interfaces;

namespace Slimer.Services
{
    public class TriviaQuestionService : ITriviaQuestionService
    {
        private readonly ITriviaQuestionRepository _repository;

        private TriviaQuestion[] _questions = default!;

        public TriviaQuestionService(ITriviaQuestionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<TriviaQuestion> GetQuestionByIdAsync(int triviaQuestionId)
        {
            return await _repository.GetTriviaQuestionByIdAsync(triviaQuestionId);
        }

        public async Task<TriviaQuestion> GetRandomQuestionAsync()
        {
            if (_questions == default || _questions.Length < 1)
                await GetQuestionsAsync();

            var rand = new Random();

            var next = rand.Next(0, _questions.Length);

            return _questions[next];
        }

        public async Task<TriviaQuestion[]> GetQuestionsAsync()
        {
            if (_questions?.Length > 0)
                return _questions;

            var results = await _repository.GetQuestionsAsync();

            _questions = results.ToArray();

            return _questions;
        }

        public async Task<TriviaQuestion> SaveAsync(TriviaQuestion question)
        {
            InvalidateCache();

            return await _repository.SaveAsync(question);
        }

        public bool InvalidateCache()
        {
            if (_questions == default || _questions.Length < 1)
                return true;

            _questions = default!;

            return true;
        }
    }
}
