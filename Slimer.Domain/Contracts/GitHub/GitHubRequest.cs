using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Domain.Contracts.GitHub
{
    [ExcludeFromCodeCoverage]
    public class GitHubRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("labels")]
        public string[] Labels { get; set; }
    }
}
