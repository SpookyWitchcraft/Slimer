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
        private readonly IGitHubService _service;

        public GitHubController(IGitHubService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(GitHubRequest request)
        {
            var issue = await _service.CreateIssueAsync(request);

            return Ok(issue);
        }
    }
}
