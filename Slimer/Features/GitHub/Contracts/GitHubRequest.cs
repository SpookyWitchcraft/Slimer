using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Slimer.Features.GitHub.Contracts
{
    [ExcludeFromCodeCoverage]
    public class GitHubRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("labels")]
        public string[] Labels { get; set; }
    }
}
