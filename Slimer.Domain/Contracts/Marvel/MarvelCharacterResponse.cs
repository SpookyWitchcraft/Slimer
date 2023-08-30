using Newtonsoft.Json;

namespace Slimer.Domain.Contracts.Marvel
{
    public class MarvelCharacterResponse
    {
        [JsonProperty("data")]
        public MarvelCharacterData Data { get; set; }
    }

    public class MarvelCharacterData
    {
        [JsonProperty("results")]
        public MarvelDataResults[] Results { get; set; }
    }

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
