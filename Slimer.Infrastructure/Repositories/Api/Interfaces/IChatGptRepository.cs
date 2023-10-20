using Slimer.Domain.Contracts.ChatGpt;

namespace Slimer.Infrastructure.Repositories.Api.Interfaces
{
    public interface IChatGptRepository
    {
        Task<GptTextResponse> GetAnswerAsync(string question);
    }
}
