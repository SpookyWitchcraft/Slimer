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
        private readonly IChatGptService _service;

        public ChatGptController(IChatGptService service)
        {
            _service = service;
        }

        [HttpGet("{question}")]
        public async Task<IActionResult> Get(string question)
        {
            var answer = await _service.AskQuestionAsync(question);

            return Ok(answer);
        }
    }
}
