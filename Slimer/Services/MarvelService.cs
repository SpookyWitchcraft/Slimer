using Slimer.Domain.Contracts.Marvel;
using Slimer.Infrastructure.Modules.Api.Interfaces;
using Slimer.Infrastructure.Services.Interfaces;
using Slimer.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Slimer.Services
{
    public class MarvelService : IMarvelService
    {
        private readonly IHttpClientProxy _client;
        private readonly ISecretsService _secretsService;

        private string GetUrl(string name, string ts, string key, string hash) => $"https://gateway.marvel.com/v1/public/characters?name={name}&ts={ts}&apikey={key}&hash={hash}";
        
        public MarvelService(IHttpClientProxy client, ISecretsService secretsService)
        {
            _client = client;
            _secretsService = secretsService;
        }

        private string CreateHash(string timeStamp, string privateKey, string publicKey)
        {
            using var md5 = MD5.Create();
            var input = Encoding.ASCII.GetBytes(timeStamp + privateKey + publicKey);
            var hash = md5.ComputeHash(input);

            return Convert.ToHexString(hash).ToLower();
        }

        public async Task<MarvelDataResults> GetCharacterDetailsAsync(string name)
        {
            var timeStamp = Guid.NewGuid().ToString();
            var publicKey = _secretsService.GetValue("MarvelPublicKey");
            var privateKey = _secretsService.GetValue("MarvelPrivateKey");

            var url = GetUrl(name, timeStamp, publicKey, CreateHash(timeStamp, privateKey, publicKey));

            var response = await _client.GetAsync<MarvelCharacterResponse>(url);

            return response.Data.Results.FirstOrDefault();
        }
    }
}
