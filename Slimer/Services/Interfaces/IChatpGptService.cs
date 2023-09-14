namespace Slimer.Services.Interfaces
{
    public interface IChatGptService
    {
        Task<IEnumerable<string>> AskQuestionAsync(string question);
    }
}
