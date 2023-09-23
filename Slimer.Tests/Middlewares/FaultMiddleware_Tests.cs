using Microsoft.AspNetCore.Http;
using Slimer.Middlewares;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Slimer.Tests.Middlewares
{
    public class FaultMiddleware_Tests
    {
        [Fact]
        public async Task FaultMiddleWare_ShouldReturn400()
        {
            var httpContext = new DefaultHttpContext();

            var ex = new BadHttpRequestException("test", 400);
            RequestDelegate mock = (_) => Task.FromException(ex);

            var mid = new FaultMiddleware(mock);

            await mid.Invoke(httpContext);

            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task FaultMiddleWare_ShouldReturn500()
        {
            var httpContext = new DefaultHttpContext();

            var ex = new ArgumentNullException();
            RequestDelegate mock = (_) => Task.FromException(ex);

            var mid = new FaultMiddleware(mock);

            await mid.Invoke(httpContext);

            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
        }
    }
}
