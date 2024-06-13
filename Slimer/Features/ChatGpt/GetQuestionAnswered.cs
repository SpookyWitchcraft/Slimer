using FluentValidation;
using Slimer.Common.Interfaces;
using Slimer.Features.ChatGpt.Contracts;
using Slimer.Features.ChatGpt.Extensions;
using Slimer.Interfaces;

namespace Slimer.Features.ChatGpt
{
    public static class GetQuestionAnswered
    {
        public record Request(string Question);
        public record Response(IEnumerable<string> Lines);

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Question).NotNull().NotEmpty().Length(1, 255);
            }
        }

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("api/chatgpt/{question}", Handler);
            }

            public static async Task<IResult> Handler(
                string question,
                IValidator<Request> validator,
                IHttpClientService clientService,
                ISecretsService secretsService)
            {
                var validationResult = await validator.ValidateAsync(new Request(question));

                if (!validationResult.IsValid)
                    throw new BadHttpRequestException(validationResult.ToString());

                var gptKey = secretsService.GetValueAsync("ChatGPTKey");

                var payload = CreatePayload(question);
                var content = clientService.CreateStringContent(payload);
                var request = clientService.CreateBearerRequest(content, HttpMethod.Post, await gptKey, "https://api.openai.com/v1/chat/completions");

                var response = await clientService.SendAsync<GptResponse>(request);

                if (response?.Choices != null && response.Choices.Count > 0)
                    return Results.Ok(new Response(response.Choices[0].Message.Content.ChunkWords()));

                return Results.Ok(new Response(["I'm a big dumb AI and couldn't figure this out 🧠"]));
            }

            private static GptRequest CreatePayload(string question)
            {
                return new GptRequest
                {
                    Messages = [new GptMessage { Content = question, Role = "user" }],
                    Model = "gpt-4o",
                    Temperature = 0.7
                };
            }
        }
    }
}
