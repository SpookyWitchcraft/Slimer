using Slimer.Controllers;
using System;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class MarvelController_Tests
    {
        [Fact]
        public void MarvelController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new MarvelController(null!));
        }
    }
}
