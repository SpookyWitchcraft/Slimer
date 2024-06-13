using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Slimer.Features.ChatGpt.Contracts
{
    [ExcludeFromCodeCoverage]
    public class GptRequest
    {
        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("messages")]
        public GptMessage[] Messages { get; set; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class GptMessage
    {
        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }
}
