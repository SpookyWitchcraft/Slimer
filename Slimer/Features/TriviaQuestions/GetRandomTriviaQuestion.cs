using Slimer.Interfaces;

namespace Slimer.Features.TriviaQuestions
{
    public static class GetRandomTriviaQuestion
    {
        public record Response(int Id, string Question, string Answer, string Category);

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("api/trivia", Handler);
            }

            public static async Task<IResult> Handler(TriviaQuestionCache cache)
            {
                var triviaQuestions = await cache.TriviaQuestions();

                var rand = new Random();

                var next = rand.Next(0, triviaQuestions.Length);

                var triviaQuestion = triviaQuestions[next];

                return Results.Ok(new Response(
                        triviaQuestion.TriviaQuestionId,
                        triviaQuestion.Question,
                        triviaQuestion.Answer,
                        triviaQuestion.Category));
            }
        }
    }
}
