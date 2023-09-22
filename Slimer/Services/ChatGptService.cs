using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Services.Interfaces;

namespace Slimer.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly IChatGptRepository _repository;

        public ChatGptService(IChatGptRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<string>> AskQuestionAsync(string question)
        {
            return await _repository.GetAnswerAsync(question);
        }
    }
}
