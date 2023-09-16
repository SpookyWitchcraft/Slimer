using Slimer.Controllers;
using System;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class ChatGptController_Tests
    {
        [Fact]
        public void ChatGptController_MissingServiceShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ChatGptController(null!));
        }
    }
}
