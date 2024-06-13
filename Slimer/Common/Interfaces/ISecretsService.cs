namespace Slimer.Common.Interfaces
{
    public interface ISecretsService
    {
        Task<string> GetValueAsync(string key);
    }
}
