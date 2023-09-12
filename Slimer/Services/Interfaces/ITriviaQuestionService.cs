using Slimer.Domain.Models.Trivia;

namespace Slimer.Services.Interfaces
{
    public interface ITriviaQuestionService
    {
        Task<IReadOnlyDictionary<int, TriviaQuestion>> GetQuestionsAsync();

        Task<TriviaQuestion> GetRandomQuestionAsync();

        Task<TriviaQuestion> SaveAsync(TriviaQuestion triviaQuestion);

        bool InvalidateCache();
    }
}
