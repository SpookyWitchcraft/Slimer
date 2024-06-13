using Azure.Core;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Caching.Memory;
using Slimer.Common.Interfaces;

namespace Slimer.Common
{
    public class SecretsService : ISecretsService
    {
        private readonly IMemoryCache _cache;
        private readonly SecretClient _secretClient;

        public SecretsService(IMemoryCache cache, SecretClient secretClient)
        {
            _cache = cache;
            _secretClient = secretClient;
        }

        public async Task<string> GetValueAsync(string key)
        {
            if (_cache.TryGetValue(key, out string? value))
                return value;

            var secretVal = ((KeyVaultSecret)(await _secretClient.GetSecretAsync(key))).Value;

            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(12));

            _cache.Set(key, secretVal, cacheOptions);

            return secretVal;
        }
    }
}
