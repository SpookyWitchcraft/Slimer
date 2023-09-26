using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MarvelController : ControllerBase
    {
        private readonly IValidator<string> _validator;
        private readonly IMarvelService _service;

        public MarvelController(IValidator<string> validator, IMarvelService service)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{characterName}")]
        public async Task<IActionResult> Get(string characterName)
        {
            var validation = await _validator.ValidateAsync(characterName);

            if (!validation.IsValid)
                throw new BadHttpRequestException(validation.ToString());

            var character = await _service.SearchForCharacterDetailsAsync(characterName);

            return Ok(character);
        }
    }
}
