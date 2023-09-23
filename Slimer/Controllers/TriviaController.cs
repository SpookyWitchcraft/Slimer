using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slimer.Domain.Models.Trivia;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly ITriviaQuestionService _service;

        public TriviaController(ITriviaQuestionService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var q = await _service.GetRandomQuestionAsync();

            return Ok(q);
        }

        [HttpGet("{triviaQuestionId}")]
        public async Task<IActionResult> Get(int triviaQuestionId)
        {
            if (triviaQuestionId < 1)
                throw new BadHttpRequestException("Trivia question must be greater than 0");

            var q = await _service.GetQuestionByIdAsync(triviaQuestionId);

            return Ok(q);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            var qs = (await _service.GetQuestionsAsync());

            return Ok(qs.Take(5).Select(x => x.Value).ToList());
        }

        [HttpGet("search/{id}")]
        public async Task<IActionResult> Search(int id)
        {
            var q = (await _service.GetQuestionsAsync())[id];

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
