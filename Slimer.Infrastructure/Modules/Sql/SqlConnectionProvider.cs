using Slimer.Infrastructure.Modules.Sql.Interfaces;
using Slimer.Infrastructure.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Infrastructure.Modules.Sql
{
    [ExcludeFromCodeCoverage]
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private readonly ISecretsService _secretsService;

        public SqlConnectionProvider(ISecretsService secretsService)
        {
            _secretsService = secretsService;
        }

        public async Task<IDbConnection> GetSqlConnection()
        {
            var connectionString = _secretsService.GetValue("SQLConnectionString");

            var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            return connection;
        }
    }
}
