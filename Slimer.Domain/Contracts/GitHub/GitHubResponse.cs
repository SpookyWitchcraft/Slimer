using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Domain.Contracts.GitHub
{
    [ExcludeFromCodeCoverage]
    public class GitHubResponse
    {
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }
}
