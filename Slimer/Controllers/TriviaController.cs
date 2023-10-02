using FluentValidation;
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
        private readonly IValidator<TriviaQuestion> _questionValidator;
        private readonly IValidator<int> _idValidator;
        private readonly ITriviaQuestionService _service;

        public TriviaController(IValidator<TriviaQuestion> questionValidator, IValidator<int> idValidator, ITriviaQuestionService service)
        {
            _questionValidator = questionValidator ?? throw new ArgumentNullException(nameof(questionValidator));
            _idValidator = idValidator ?? throw new ArgumentNullException(nameof(idValidator));
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
            var validation = await _idValidator.ValidateAsync(triviaQuestionId);

            if (!validation.IsValid)
                throw new BadHttpRequestException(validation.ToString());

            var q = await _service.GetQuestionByIdAsync(triviaQuestionId);

            return Ok(q);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            var qs = (await _service.GetQuestionsAsync());

            return Ok(qs.Take(5).Select(x => x).ToList());
        }
        
        [HttpGet("search/{id}")]
        public async Task<IActionResult> Search(int id)
        {
            var validation = await _idValidator.ValidateAsync(id);

            if (!validation.IsValid)
                throw new BadHttpRequestException(validation.ToString());

            var q = (await _service.GetQuestionsAsync()).First(x => x.Id == id);

            return Ok(q);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(TriviaQuestion question)
        {
            var validation = await _questionValidator.ValidateAsync(question);

            if (!validation.IsValid)
                throw new BadHttpRequestException(validation.ToString());

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
