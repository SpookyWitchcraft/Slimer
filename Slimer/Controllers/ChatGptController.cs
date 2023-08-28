using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
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
            var answer = await _service.GetAnswerAsync(question);

            return Ok(answer);
        }
    }
}
