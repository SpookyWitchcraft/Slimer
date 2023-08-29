using Slimer.Domain.Contracts.Marvel;

namespace Slimer.Services.Interfaces
{
    public interface IMarvelService
    {
        Task<MarvelDataResults> GetCharacterDetailsAsync(string name);
    }
}
