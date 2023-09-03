using Slimer.Domain.Models.Trivia;
using Slimer.Infrastructure.Modules.Sql.Interfaces;
using Slimer.Infrastructure.Repositories.Sql.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Slimer.Infrastructure.Repositories.Sql
{
    public class TriviaQuestionRepository : ITriviaQuestionRepository
    {
        private readonly ISqlExecutor _executor;

        public TriviaQuestionRepository(ISqlExecutor executor)
        {
            _executor = executor;
        }

        public async Task<ICollection<TriviaQuestion>> GetQuestionsAsync()
        {
            try
            {
                return await _executor.ReadList(LoadProperties, "GetTriviaQuestions");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return default!;
            }
        }

        public async Task<TriviaQuestion> SaveAsync(TriviaQuestion triviaQuestion)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                        new SqlParameter("@Id", SqlDbType.Int) { Value = triviaQuestion.Id, Direction = ParameterDirection.Input },
                        new SqlParameter("@Category", SqlDbType.VarChar) { Value = triviaQuestion.Category, Direction = ParameterDirection.Input },
                        new SqlParameter("@Question", SqlDbType.VarChar) { Value = triviaQuestion.Question, Direction = ParameterDirection.Input },
                        new SqlParameter("@Answer", SqlDbType.VarChar) { Value = triviaQuestion.Answer, Direction = ParameterDirection.Input },
                        new SqlParameter("@IsEnabled", SqlDbType.Bit) { Value = triviaQuestion.IsEnabled, Direction = ParameterDirection.Input },
                    };

                var results = await _executor.Write("UpdateTriviaQuestion", parameters);

                return triviaQuestion;
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
