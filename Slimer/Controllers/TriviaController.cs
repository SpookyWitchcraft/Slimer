using Microsoft.AspNetCore.Mvc;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly ITriviaService _service;

        public TriviaController(ITriviaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var q = _service.GetQuestion();

            return Ok(q);
        }
    }
}
