using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Slimer.Common;
using Slimer.Features.TriviaQuestions.Contracts;

namespace Slimer.Features.TriviaQuestions
{
    public class TriviaQuestionCache
    {
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;

        public TriviaQuestionCache(IMemoryCache cache, AppDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<TriviaQuestion[]> TriviaQuestions()
        {
            if (_cache.TryGetValue("triviaQuestions", out TriviaQuestion[]? existing))
                return existing;

            var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(2));

            var questions = await _context.TriviaQuestions.ToArrayAsync();

            _cache.Set("triviaQuestions", questions, options);

            return questions;
        }
    }
}
