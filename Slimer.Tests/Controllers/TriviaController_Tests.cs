using Slimer.Controllers;
using System;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class TriviaController_Tests
    {
        [Fact]
        public void TriviaController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new TriviaController(null!));
        }
    }
}
