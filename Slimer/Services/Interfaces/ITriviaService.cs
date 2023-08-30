using Slimer.Domain.Contracts.Trivia;

namespace Slimer.Services.Interfaces
{
    public interface ITriviaService
    {
        Task<TriviaQuestion> GetQuestionAsync();
    }
}
