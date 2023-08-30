using Slimer.Infrastructure.Services.Interfaces;

namespace Slimer.Infrastructure.Services
{
    public class LocalService : ISecretsService
    {
        public Dictionary<string, string> Secrets { get; }

        public string GetValue(string key)
        {
            throw new NotImplementedException();
        }
    }
}
