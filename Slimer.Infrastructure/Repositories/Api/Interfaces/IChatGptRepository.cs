namespace Slimer.Infrastructure.Repositories.Api.Interfaces
{
    public interface IChatGptRepository
    {
        Task<IEnumerable<string>> GetAnswerAsync(string question);
    }
}
