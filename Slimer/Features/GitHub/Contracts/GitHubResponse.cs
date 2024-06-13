using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Slimer.Features.GitHub.Contracts
{
    [ExcludeFromCodeCoverage]
    public class GitHubResponse
    {
        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }
    }
}
