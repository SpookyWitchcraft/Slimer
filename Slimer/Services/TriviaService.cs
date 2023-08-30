using Slimer.Domain.Contracts.Trivia;
using Slimer.Infrastructure.Modules.Sql.Interfaces;
using Slimer.Services.Interfaces;
using System.Data;

namespace Slimer.Services
{
    public class TriviaService : ITriviaService
    {
        private readonly ISqlExecutor _executor;

        private TriviaQuestion[] _questions;

        public TriviaService(ISqlExecutor executor)
        {
            _executor = executor;
        }

        public async Task<TriviaQuestion> GetQuestionAsync()
        {
            if (_questions == null || _questions.Length < 1)
                await GetQuestionsAsync();

            var rand = new Random();

            var next = rand.Next(0, _questions.Length);

            return _questions[next];
        }

        public async Task<ICollection<TriviaQuestion>> GetQuestionsAsync()
        {
            try
            {
                var results = await _executor.ReadList(LoadProperties, "GetTriviaQuestions");

                _questions = results.ToArray();

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return default!;
            }
        }

        private TriviaQuestion LoadProperties(IDataReader reader)
        {
            return new TriviaQuestion(
                Bind<int>(reader, "TriviaQuestionId")
                , Bind<string>(reader, "Question")
                , Bind<string>(reader, "Answer")
                , Bind<string>(reader, "Category")
                , Bind<bool>(reader, "IsEnabled")
                , Bind<DateTime>(reader, "CreatedDate")
                , Bind<DateTime>(reader, "UpdatedDate")
                );
        }

        public static T Bind<T>(IDataReader reader, string column)
        {
            var index = reader.GetOrdinal(column);

            return !reader.IsDBNull(index)
                ? (T)reader[column]
                : default!;
        }
    }
}
