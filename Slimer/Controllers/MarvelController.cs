using Microsoft.AspNetCore.Mvc;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarvelController : ControllerBase
    {
        private readonly IMarvelService _service;

        public MarvelController(IMarvelService service)
        {
            _service = service;
        }

        [HttpGet("{characterName}")]
        public async Task<IActionResult> Get(string characterName)
        {
            var character = await _service.GetCharacterDetailsAsync(characterName);

            return Ok(character);
        }
    }
}
