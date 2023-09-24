using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slimer.Domain.Contracts.GitHub;
using Slimer.Services.Interfaces;

namespace Slimer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private IValidator<GitHubRequest> _validator;
        private readonly IGitHubService _service;

        public GitHubController(IValidator<GitHubRequest> validator, IGitHubService service)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        public async Task<IActionResult> Post(GitHubRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if(!validation.IsValid)
                throw new BadHttpRequestException(validation.ToString());

            var issue = await _service.CreateIssueAsync(request);

            return Ok(issue);
        }
    }
}
