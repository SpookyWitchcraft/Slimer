using Microsoft.AspNetCore.Mvc;
using Slimer.Infrastructure.Services;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            var message = SecretsService.TryIt();

            return Ok(message);
        }
    }
}
