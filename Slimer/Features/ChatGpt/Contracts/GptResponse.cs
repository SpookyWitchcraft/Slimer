using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Slimer.Features.ChatGpt.Contracts
{
    [ExcludeFromCodeCoverage]
    public class GptResponse
    {
        [JsonPropertyName("choices")]
        public List<GptChoice> Choices { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class GptChoice
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("message")]
        public GptResponseMessage Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class GptResponseMessage
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
