using Slimer.Infrastructure.Services;
using Xunit;

namespace Slimer.Infrastructure.Tests.Services
{
    public class SecretsService_Tests
    {
        [Fact]
        public void GetValue_ShouldReturnValue()
        {
            var service = new SecretsService();

            service.Secrets.Add("Key", "Value");

            var results = service.GetValue("Key");

            Assert.Equal("Value", results);
        }
    }
}
