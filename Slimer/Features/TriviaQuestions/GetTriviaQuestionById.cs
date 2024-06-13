using FluentValidation;
using Slimer.Common;
using Slimer.Features.TriviaQuestions.Contracts;
using Slimer.Interfaces;

namespace Slimer.Features.TriviaQuestions
{
    public static class GetTriviaQuestionById
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
                app.MapGet("api/trivia/{id}", Handler);
            }

            public static async Task<IResult> Handler(int id, IValidator<Request> validator, AppDbContext context)
            {
                var validationResult = await validator.ValidateAsync(new Request(id));

                if (!validationResult.IsValid)
                    throw new BadHttpRequestException(validationResult.ToString());

                var triviaQuestion = await context.TriviaQuestions.FindAsync(id);

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
