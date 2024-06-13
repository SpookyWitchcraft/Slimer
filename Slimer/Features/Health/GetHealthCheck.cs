using Slimer.Interfaces;

namespace Slimer.Features.Health
{
    public static class GetHealthCheck
    {
        public static class GetTriviaQuestionById
        {
            public class Endpoint : IEndpoint
            {
                public void MapEndpoint(IEndpointRouteBuilder app)
                {
                    app.MapGet("api/health", Handler).AllowAnonymous();
                }

                public static IResult Handler()
                {
                    return Results.Ok();
                }
            }
        }
    }
}
