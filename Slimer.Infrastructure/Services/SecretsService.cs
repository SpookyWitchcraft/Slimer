using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Slimer.Infrastructure.Services.Interfaces;

namespace Slimer.Infrastructure.Services
{
    public class SecretsService : ISecretsService
    {
        public Dictionary<string, string> Secrets { get; } = new Dictionary<string, string>();

        public string GetValue(string key)
        {
            if (Secrets.ContainsKey(key))
                return Secrets[key];

            var options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };
            var client = new SecretClient(new Uri("https://spookywitchcraft-vault.vault.azure.net/"), new DefaultAzureCredential(), options);

            return ((KeyVaultSecret)client.GetSecret(key)).Value;
        }
    }
}
