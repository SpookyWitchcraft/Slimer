using Slimer.Domain.Contracts.Marvel;

namespace Slimer.Infrastructure.Repositories.Api.Interfaces
{
    public interface IMarvelRepository
    {
        Task<MarvelDataResults> GetCharacterDetailsAsync(string name);
    }
}
