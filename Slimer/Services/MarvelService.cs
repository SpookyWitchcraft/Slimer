using Slimer.Contracts.Marvel;
using Slimer.Repositories.Interfaces;
using Slimer.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Slimer.Services
{
    public class MarvelService : IMarvelService
    {
        private readonly IHttpClientProxy _client;

        private string TimeStamp;
        private string PublicKey = Environment.GetEnvironmentVariable("publicKey");
        private string PrivateKey = Environment.GetEnvironmentVariable("privateKey");

        private string GetUrl(string name, string ts, string key, string hash) => $"https://gateway.marvel.com/v1/public/characters?name={name}&ts={ts}&apikey={key}&hash={hash}";
        
        public MarvelService(IHttpClientProxy client)
        {
            _client = client;
        }

        private string CreateHash()
        {
            using var md5 = MD5.Create();
            var input = Encoding.ASCII.GetBytes(TimeStamp + PrivateKey + PublicKey);
            var hash = md5.ComputeHash(input);

            return Convert.ToHexString(hash).ToLower();
        }

        public async Task<MarvelDataResults> GetCharacterDetailsAsync(string name)
        {
            TimeStamp = Guid.NewGuid().ToString();

            var url = GetUrl(name, TimeStamp, PublicKey, CreateHash());

            var response = await _client.GetAsync<MarvelCharacterResponse>(url);

            return response.Data.Results.FirstOrDefault();
        }
    }
}
