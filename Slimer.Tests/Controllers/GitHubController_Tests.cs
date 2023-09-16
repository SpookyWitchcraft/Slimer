using Slimer.Controllers;
using System;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class GitHubController_Tests
    {
        [Fact]
        public void GitHubController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new GitHubController(null!));
        }
    }
}
