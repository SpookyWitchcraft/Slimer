namespace Slimer.Services.Interfaces
{
    public interface IChatGptService
    {
        Task<IEnumerable<string>> AskAnswerAsync(string question);
    }
}
