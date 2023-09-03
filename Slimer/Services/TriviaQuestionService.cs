using Slimer.Domain.Models.Trivia;
using Slimer.Infrastructure.Repositories.Sql.Interfaces;
using Slimer.Services.Interfaces;

namespace Slimer.Services
{
    public class TriviaQuestionService : ITriviaQuestionService
    {
        private readonly ITriviaQuestionRepository _repository;

        private TriviaQuestion[] _questions;

        public TriviaQuestionService(ITriviaQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<TriviaQuestion> GetQuestionAsync()
        {
            if (_questions == null || _questions.Length < 1)
                await _repository.GetQuestionsAsync();

            var rand = new Random();

            var next = rand.Next(0, _questions!.Length);

            return _questions[next];
        }

        public async Task<ICollection<TriviaQuestion>> GetQuestionsAsync()
        {
            if (_questions != null && _questions.Length > 0)
                return _questions;

            var results = await _repository.GetQuestionsAsync();

            _questions = results.ToArray();

            return results;
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
                if (_questions == null || _questions.Length < 1)
                    return true;

                _questions = Array.Empty<TriviaQuestion>();

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
