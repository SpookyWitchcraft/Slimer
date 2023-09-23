using Slimer.Infrastructure.Modules.Sql.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Infrastructure.Modules.Sql
{
    [ExcludeFromCodeCoverage]
    public class SqlCommandProvider : ISqlCommandProvider
    {
        public IDbCommand CreateCommand(IDbConnection connection, string storedProcedure)
        {
            return new SqlCommand(storedProcedure, (SqlConnection)connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 300
            };
        }

        public async Task ExecuteNonQueryAsync(IDbCommand command)
        {
            await ((SqlCommand)command).ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> ExecuteReaderAsync(IDbCommand command)
        {
            return await ((SqlCommand)command).ExecuteReaderAsync();
        }
    }
}
