using Slimer.Domain.Models.Trivia;

namespace Slimer.Infrastructure.Repositories.Sql.Interfaces
{
    public interface ITriviaQuestionRepository
    {
        Task<TriviaQuestion> GetTriviaQuestionByIdAsync(int triviaQuestionId);

        Task<ICollection<TriviaQuestion>> GetQuestionsAsync();

        Task<TriviaQuestion> SaveAsync(TriviaQuestion triviaQuestion);
    }
}
