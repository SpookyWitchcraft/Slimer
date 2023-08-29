using System.Data;
using System.Data.SqlClient;

namespace Slimer.Infrastructure.Modules.Sql.Interfaces
{
    public interface ISqlExecutor
    {
        Task Execute(string storedProcedure, IEnumerable<SqlParameter> parameters = default!);

        Task<List<T>> ReadList<T>(Func<IDataReader, T> loader, string storedProcedure, IEnumerable<SqlParameter> parameters = default!) where T : new();

        Task<T> Read<T>(Func<IDataReader, T> loader, string storedProcedure, IEnumerable<SqlParameter> parameters = default!);

        Task<bool> Write(string storedProcedure, IEnumerable<SqlParameter> parameters);

        Task<bool> WriteList(string storedProcedure, IEnumerable<IEnumerable<SqlParameter>> parameters);
    }
}
