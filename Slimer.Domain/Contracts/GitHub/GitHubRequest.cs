using Newtonsoft.Json;

namespace Slimer.Domain.Contracts.GitHub
{
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
