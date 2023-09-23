using System.Net;
using System.Text.Json;

namespace Slimer.Middlewares
{
    public class FaultMiddleware
    {
        private readonly RequestDelegate _next;

        public FaultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });

                await response.WriteAsync(result);
            }
        }
    }
}
