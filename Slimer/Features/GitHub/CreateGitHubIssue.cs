using FluentValidation;
using Slimer.Common.Interfaces;
using Slimer.Features.GitHub.Contracts;
using Slimer.Interfaces;

namespace Slimer.Features.GitHub
{
    public static class CreateGitHubIssue
    {
        public record Request(string Title, string Body, string[] Labels);
        public record Response(string HtmlUrl);

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty().Length(1, 255);
                RuleFor(x => x.Body).NotEmpty().Length(1, 255);
                RuleFor(x => x.Labels).NotNull().NotEmpty();
                RuleForEach(x => x.Labels)
                    .NotNull()
                    .NotEmpty()
                    .Must(x => string.Equals(x, "BUG", StringComparison.OrdinalIgnoreCase));
            }
        }

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPost("api/github", Handler);
            }

            public static async Task<IResult> Handler(
                Request request,
                IValidator<Request> validator,
                ISecretsService secretsService,
                IHttpClientService clientService)
            {
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                    throw new BadHttpRequestException(validationResult.ToString());

                var token = secretsService.GetValueAsync("GitHubToken");

                var headers = new List<(string, string)> { ("Accept", "application/vnd.github+json"), ("X-GitHub-Api-Version", "2022-11-28"), ("User-Agent", "Slimer") };

                var obj = new
                {
                    title = request.Title,
                    body = request.Body,
                    labels = request.Labels
                };

                //camelcase issue
                var content = clientService.CreateStringContent(obj);
                var httpRequest = clientService.CreateBearerRequest(content, HttpMethod.Post, await token, Environment.GetEnvironmentVariable("E_ISSUES_URL"), headers);

                var response = await clientService.SendAsync<GitHubResponse>(httpRequest);

                return response != null
                    ? Results.Ok(new Response(response.HtmlUrl))
                    : Results.BadRequest();
            }
        }
    }
}
