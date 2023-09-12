using Slimer.Domain.Contracts.Marvel;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Services.Interfaces;

namespace Slimer.Services
{
    public class MarvelService : IMarvelService
    {
        private readonly IMarvelRepository _repository;

        public MarvelService(IMarvelRepository repository)
        {
            _repository = repository;
        }

        public async Task<MarvelDataResults> SearchForCharacterDetailsAsync(string name)
        {
            return await _repository.GetCharacterDetailsAsync(name);
        }
    }
}
