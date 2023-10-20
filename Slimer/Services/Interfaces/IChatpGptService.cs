using Slimer.Domain.Contracts.ChatGpt;

namespace Slimer.Services.Interfaces
{
    public interface IChatGptService
    {
        Task<GptTextResponse> AskQuestionAsync(string question);
    }
}
