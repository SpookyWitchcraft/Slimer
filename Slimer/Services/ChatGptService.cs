using Newtonsoft.Json;
using Slimer.Domain.Contracts.ChatGpt;
using Slimer.Infrastructure.Modules.Api.Interfaces;
using Slimer.Infrastructure.Services.Interfaces;
using Slimer.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace Slimer.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly IHttpClientProxy _client;
        private readonly ISecretsService _secretsService;

        public ChatGptService(IHttpClientProxy client, ISecretsService secretsService)
        {
            _client = client;
            _secretsService = secretsService;
        }

        public async Task<IEnumerable<string>> GetAnswerAsync(string question)
        {
            var request = CreateRequest(CreateContent(question), "https://api.openai.com/v1/chat/completions");

            var response = await _client.SendAsync<GptResponse>(request);

            if (response?.Choices != null && response.Choices.Count > 0)
                return response.Choices[0].Message.Content.Chunk(120).Select(x => new string(x));

            var def = new string[] { "I'm a big dumb AI and couldn't figure this out 🧠" };
            
            return def;
        }

        private StringContent CreateContent(string question)
        {
            var payload = CreatePayload(question);

            var serialized = JsonConvert.SerializeObject(payload);

            return new StringContent(serialized, Encoding.UTF8, "application/json");
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

        private HttpRequestMessage CreateRequest(HttpContent content, string url)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri(url),
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _secretsService.GetValue("ChatGPTKey"));

            return request;
        }
    }
}
