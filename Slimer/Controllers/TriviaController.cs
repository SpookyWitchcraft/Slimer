using Microsoft.AspNetCore.Mvc;
using Slimer.Domain.Models.Trivia;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly ITriviaQuestionService _service;

        public TriviaController(ITriviaQuestionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var q = await _service.GetQuestionAsync();

            return Ok(q);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            var qs = (await _service.GetQuestionsAsync());

            return Ok(qs.Take(100));
        }

        [HttpGet("search/{id}")]
        public async Task<IActionResult> Search(int id)
        {
            var q = (await _service.GetQuestionsAsync()).First(x => x.Id == id);

            return Ok(q);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(TriviaQuestion question)
        {
            var results = await _service.SaveAsync(question);

            return Ok(results);
        }

        [HttpGet("invalidate")]
        public IActionResult Invalidate()
        {
            var results = _service.InvalidateCache();

            return Ok(results);
        }
    }
}
