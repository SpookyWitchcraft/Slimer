using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly IValidator<string> _validator;
        private readonly IChatGptService _service;

        public ChatGptController(IValidator<string> validator, IChatGptService service)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{question}")]
        public async Task<IActionResult> Get(string question)
        {
            var validation = await _validator.ValidateAsync(question);

            if (!validation.IsValid)
                throw new BadHttpRequestException(validation.ToString());

            var answer = await _service.AskQuestionAsync(question);

            return Ok(answer);
        }
    }
}
