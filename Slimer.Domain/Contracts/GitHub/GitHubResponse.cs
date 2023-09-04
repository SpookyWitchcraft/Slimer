using Newtonsoft.Json;

namespace Slimer.Domain.Contracts.GitHub
{
    public class GitHubResponse
    {
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }
}
