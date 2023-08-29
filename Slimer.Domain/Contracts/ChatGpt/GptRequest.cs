using Newtonsoft.Json;

namespace Slimer.Domain.Contracts.ChatGpt
{
    public class GptRequest
    {
        [JsonProperty("model")]
        public string? Model { get; set; }

        [JsonProperty("messages")]
        public GptMessage[] Messages { get; set; }

        [JsonProperty("temperature")]
        public double? Temperature { get; set; }
    }

    public class GptMessage
    {
        [JsonProperty("role")]
        public string? Role { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }
    }
}
