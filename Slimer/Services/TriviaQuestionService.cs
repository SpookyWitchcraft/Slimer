using Slimer.Domain.Models.Trivia;
using Slimer.Infrastructure.Repositories.Sql.Interfaces;
using Slimer.Services.Interfaces;

namespace Slimer.Services
{
    public class TriviaQuestionService : ITriviaQuestionService
    {
        private readonly ITriviaQuestionRepository _repository;

        private IReadOnlyDictionary<int, TriviaQuestion> _questions;

        public TriviaQuestionService(ITriviaQuestionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _questions = new Dictionary<int, TriviaQuestion>();
        }

        public async Task<TriviaQuestion> GetQuestionByIdAsync(int triviaQuestionId)
        {
            return await _repository.GetTriviaQuestionByIdAsync(triviaQuestionId);
        }

        public async Task<TriviaQuestion> GetRandomQuestionAsync()
        {
            if (_questions.Count < 1)
                await GetQuestionsAsync();

            var rand = new Random();

            var next = rand.Next(0, _questions!.Count);

            return _questions[next];
        }

        public async Task<IReadOnlyDictionary<int, TriviaQuestion>> GetQuestionsAsync()
        {
            if (_questions != null && _questions.Count > 0)
                return _questions;

            var results = await _repository.GetQuestionsAsync();

            _questions = results.ToDictionary(x => x.Id, x => x);

            return _questions;
        }

        public async Task<TriviaQuestion> SaveAsync(TriviaQuestion question)
        {
            InvalidateCache();

            return await _repository.SaveAsync(question);
        }

        public bool InvalidateCache()
        {
            try
            {
                if (_questions.Count < 1)
                    return true;

                _questions = new Dictionary<int, TriviaQuestion>();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
