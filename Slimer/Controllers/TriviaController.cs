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
        public async Task<IActionResult> Get()
        {
            var q = await _service.GetQuestionAsync();

            return Ok(q);
        }
    }
}
