namespace Slimer.Infrastructure.Services.Interfaces
{
    public interface ISecretsService
    {
        Dictionary<string, string> Secrets { get; }

        string GetValue(string key);
    }
}
