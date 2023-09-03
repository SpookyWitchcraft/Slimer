using Slimer.Domain.Models.Trivia;

namespace Slimer.Infrastructure.Repositories.Sql.Interfaces
{
    public interface ITriviaQuestionRepository
    {
        Task<ICollection<TriviaQuestion>> GetQuestionsAsync();

        Task<TriviaQuestion> SaveAsync(TriviaQuestion triviaQuestion);
    }
}
