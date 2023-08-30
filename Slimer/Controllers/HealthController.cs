using Microsoft.AspNetCore.Mvc;
using Slimer.Domain.Contracts.Marvel;
using Slimer.Infrastructure.Modules.Api;
using Slimer.Services;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            var a = new MarvelService(default);
            var b = new MarvelCharacterResponse();

            var c = new HttpClientProxy(null);

            var message = $"a={a} b={b} c={c}";

            return Ok(message);
        }
    }
}
