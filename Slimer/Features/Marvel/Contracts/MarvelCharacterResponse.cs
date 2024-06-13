using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Slimer.Features.Marvel.Contracts
{
    [ExcludeFromCodeCoverage]
    public class MarvelCharacterResponse
    {
        [JsonPropertyName("data")]
        public MarvelCharacterData Data { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MarvelCharacterData
    {
        [JsonPropertyName("results")]
        public MarvelDataResults[] Results { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MarvelDataResults
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
