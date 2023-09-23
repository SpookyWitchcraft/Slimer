using Microsoft.AspNetCore.Mvc;
using Slimer.Controllers;
using Xunit;

namespace Slimer.Tests.Controllers
{
    public class HealthController_Tests
    {
        [Fact]
        public void Get_ShouldReturn200()
        {
            var controller = new HealthController();

            var response = controller.Get() as OkResult;

            Assert.NotNull(response);
            Assert.Equal(200, response?.StatusCode);
        }
    }
}
