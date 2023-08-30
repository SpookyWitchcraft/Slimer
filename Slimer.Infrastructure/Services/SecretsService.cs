using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Slimer.Infrastructure.Services.Interfaces;

namespace Slimer.Infrastructure.Services
{
    public class SecretsService : ISecretsService
    {
        public static string TryIt()
        {
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

            KeyVaultSecret secret = client.GetSecret("testSecret");

            return secret.Value;
        }
    }
}
