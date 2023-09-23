using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Domain.Contracts.ChatGpt
{
    [ExcludeFromCodeCoverage]
    public class GptResponse
    {
        [JsonProperty("choices")]
        public List<GptChoice> Choices { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class GptChoice
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("message")]
        public GptResponseMessage Message { get; set; }

        [JsonProperty("finish_reson")]
        public string FinishReason { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class GptResponseMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}