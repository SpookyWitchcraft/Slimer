using Slimer.Domain.Models.Trivia;

namespace Slimer.Services.Interfaces
{
    public interface ITriviaQuestionService
    {
        Task<ICollection<TriviaQuestion>> GetQuestionsAsync();

        Task<TriviaQuestion> GetQuestionAsync();

        Task<TriviaQuestion> SaveAsync(TriviaQuestion triviaQuestion);

        bool InvalidateCache();
    }
}
