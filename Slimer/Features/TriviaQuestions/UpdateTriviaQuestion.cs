using FluentValidation;
using Slimer.Common;
using Slimer.Features.TriviaQuestions.Contracts;
using Slimer.Interfaces;

namespace Slimer.Features.TriviaQuestions
{
    public static class UpdateTriviaQuestion
    {
        public record Request(int Id, string Question, string Answer, string Category);
        public record Response(int Id, string Question, string Answer, string Category);

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Answer).NotNull().NotEmpty().Length(1, 5000);
                RuleFor(x => x.Question).NotNull().NotEmpty().Length(1, 5000);
                RuleFor(x => x.Category).NotNull().NotEmpty().Length(1, 255);
                RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue);
            }
        }

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPut("api/trivia", Handler);
            }

            public static async Task<IResult> Handler(Request request, IValidator<Request> validator, AppDbContext context)
            {
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                    throw new BadHttpRequestException(validationResult.ToString());

                var triviaQuestion = await context.TriviaQuestions.FindAsync(request.Id);

                if (triviaQuestion == null)
                    return Results.NotFound();

                triviaQuestion.Answer = request.Answer;
                triviaQuestion.Category = request.Category;
                triviaQuestion.Question = request.Question;

                await context.SaveChangesAsync();

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
