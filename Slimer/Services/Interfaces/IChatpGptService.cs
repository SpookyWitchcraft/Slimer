namespace Slimer.Services.Interfaces
{
    public interface IChatGptService
    {
        Task<IEnumerable<char[]>> GetAnswerAsync(string question);
    }
}
