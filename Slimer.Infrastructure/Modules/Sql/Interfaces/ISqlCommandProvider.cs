using System.Data;

namespace Slimer.Infrastructure.Modules.Sql.Interfaces
{
    public interface ISqlCommandProvider
    {
        IDbCommand CreateCommand(IDbConnection connection, string storedProcedure);

        Task ExecuteNonQueryAsync(IDbCommand command);

        Task<IDataReader> ExecuteReaderAsync(IDbCommand command);
    }
}
