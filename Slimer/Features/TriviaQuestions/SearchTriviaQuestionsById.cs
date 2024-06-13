using FluentValidation;
using Slimer.Features.TriviaQuestions.Contracts;
using Slimer.Interfaces;

namespace Slimer.Features.TriviaQuestions
{
    public static class SearchTriviaQuestionsById
    {
        public record Request(int Id);
        public record Response(int Id, string Question, string Answer, string Category);

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue);
            }
        }

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("api/trivia/search/{id}", Handler);
            }

            public static async Task<IResult> Handler(int id, IValidator<Request> validator, TriviaQuestionCache cache)
            {
                var validationResult = await validator.ValidateAsync(new Request(id));

                if (!validationResult.IsValid)
                    throw new BadHttpRequestException(validationResult.ToString());

                var triviaQuestion = (await cache.TriviaQuestions())[id - 1];

                return triviaQuestion is TriviaQuestion
                    ? Results.Ok(new Response(
                        triviaQuestion.TriviaQuestionId,
                        triviaQuestion.Question,
                        triviaQuestion.Answer,
                        triviaQuestion.Category))
                    : Results.NotFound();
            }
        }
    }
}
