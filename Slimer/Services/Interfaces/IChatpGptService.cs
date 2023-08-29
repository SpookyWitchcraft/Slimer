namespace Slimer.Services.Interfaces
{
    public interface IChatGptService
    {
        Task<IEnumerable<string>> GetAnswerAsync(string question);
    }
}
