using System.Data;

namespace Slimer.Infrastructure.Modules.Sql.Interfaces
{
    public interface ISqlConnectionProvider
    {
        Task<IDbConnection> GetSqlConnection();
    }
}
