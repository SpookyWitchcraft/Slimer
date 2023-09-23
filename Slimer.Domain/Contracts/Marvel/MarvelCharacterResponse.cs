using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Domain.Contracts.Marvel
{
    [ExcludeFromCodeCoverage]
    public class MarvelCharacterResponse
    {
        [JsonProperty("data")]
        public MarvelCharacterData Data { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MarvelCharacterData
    {
        [JsonProperty("results")]
        public MarvelDataResults[] Results { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MarvelDataResults
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
