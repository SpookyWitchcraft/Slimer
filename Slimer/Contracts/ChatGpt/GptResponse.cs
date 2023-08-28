using Newtonsoft.Json;

namespace Slimer.Contracts.ChatGpt
{
    public class GptResponse
    {
        [JsonProperty("choices")]
        public List<GptChoice> Choices { get; set; }
    }

    public class GptChoice
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("message")]
        public GptResponseMessage Message { get; set; }

        [JsonProperty("finish_reson")]
        public string FinishReason { get; set; }
    }

    public class GptResponseMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}