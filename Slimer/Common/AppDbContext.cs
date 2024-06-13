using Microsoft.EntityFrameworkCore;
using Slimer.Features.TriviaQuestions.Contracts;

namespace Slimer.Common
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TriviaQuestion> TriviaQuestions => Set<TriviaQuestion>();
    }
}
