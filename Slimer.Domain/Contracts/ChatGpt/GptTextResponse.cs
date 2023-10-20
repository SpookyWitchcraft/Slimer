using System.Diagnostics.CodeAnalysis;

namespace Slimer.Domain.Contracts.ChatGpt
{
    [ExcludeFromCodeCoverage]
    public class GptTextResponse
    {
        public IEnumerable<string> Lines { get; set; }
    }
}
