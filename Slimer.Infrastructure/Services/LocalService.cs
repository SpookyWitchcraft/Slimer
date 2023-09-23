using Slimer.Infrastructure.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Infrastructure.Services
{
    //remove this when complete
    [ExcludeFromCodeCoverage]
    public class LocalService : ISecretsService
    {
        public Dictionary<string, string> Secrets { get; }

        public string GetValue(string key)
        {
            throw new NotImplementedException();
        }
    }
}
