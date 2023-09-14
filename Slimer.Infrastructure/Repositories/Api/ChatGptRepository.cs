using Slimer.Domain.Contracts.ChatGpt;
using Slimer.Infrastructure.Extensions;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Infrastructure.Services.Interfaces;

namespace Slimer.Infrastructure.Repositories.Api
{
    public class ChatGptRepository : IChatGptRepository
    {
        private readonly IHttpClientService _client;
        private readonly ISecretsService _secretsService;

        public ChatGptRepository(IHttpClientService client, ISecretsService secretsService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _secretsService = secretsService ?? throw new ArgumentNullException(nameof(secretsService));
        }

        public async Task<IEnumerable<string>> GetAnswerAsync(string question)
        {
            var payload = CreatePayload(question);
            var content = _client.CreateStringContent(payload);
            var request = _client.CreateBearerRequest(content, HttpMethod.Post, _secretsService.GetValue("ChatGPTKey"), "https://api.openai.com/v1/chat/completions");

            var response = await _client.SendAsync<GptResponse>(request);

            if (response?.Choices != null && response.Choices.Count > 0)
                return response.Choices[0].Message.Content.ChunkWords();

            return new string[] { "I'm a big dumb AI and couldn't figure this out 🧠" };
        }

        private GptRequest CreatePayload(string question)
        {
            return new GptRequest
            {
                Messages = new[] { new GptMessage { Content = question, Role = "user" } },
                Model = "gpt-3.5-turbo",
                Temperature = 0.7
            };
        }
    }
}
