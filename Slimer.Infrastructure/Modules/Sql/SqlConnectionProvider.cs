using Slimer.Infrastructure.Modules.Sql.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Infrastructure.Modules.Sql
{
    [ExcludeFromCodeCoverage]
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private readonly string _connectionString;

        public SqlConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> GetSqlConnection()
        {
            var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return connection;
        }
    }
}
