using Slimer.Infrastructure.Modules.Sql.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Slimer.Infrastructure.Modules.Sql
{
    [ExcludeFromCodeCoverage]
    public class SqlExecutor : ISqlExecutor
    {
        private readonly ISqlConnectionProvider _connection;
        private readonly ISqlCommandProvider _command;

        public SqlExecutor(ISqlConnectionProvider connection, ISqlCommandProvider command)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

            _command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public async Task Execute(string storedProcedure, IEnumerable<SqlParameter> parameters = default!)
        {
            using var connection = await _connection.GetSqlConnection();
            using var command = _command.CreateCommand(connection, storedProcedure);

            AddParameters(command, parameters);

            await _command.ExecuteNonQueryAsync(command);
        }

        public async Task<List<T>> ReadList<T>(Func<IDataReader, T> loader, string storedProcedure, IEnumerable<SqlParameter> parameters = default!)
        {
            var result = new List<T>();

            using (var connection = await _connection.GetSqlConnection())
            {
                using var command = _command.CreateCommand(connection, storedProcedure);

                AddParameters(command, parameters);

                result = await ExecuteReaderResultForList(loader, command, result);
            }

            return result;
        }

        public async Task<T> Read<T>(Func<IDataReader, T> loader, string storedProcedure, IEnumerable<SqlParameter> parameters = default!)
        {
            T result = default!;

            using (var connection = await _connection.GetSqlConnection())
            {
                using var command = _command.CreateCommand(connection, storedProcedure);

                AddParameters(command, parameters);

                result = await ExecuteReaderResult(loader, command, result);
            }

            return result;
        }

        public async Task<bool> Write(string storedProcedure, IEnumerable<SqlParameter> parameters)
        {
            using (var connection = await _connection.GetSqlConnection())
            {
                using var command = _command.CreateCommand(connection, storedProcedure);

                AddParameters(command, parameters);

                await _command.ExecuteNonQueryAsync(command);
            }

            return true;
        }

        public async Task<bool> WriteList(string storedProcedure, IEnumerable<IEnumerable<SqlParameter>> parameters)
        {
            using (var connection = await _connection.GetSqlConnection())
            {
                using var command = _command.CreateCommand(connection, storedProcedure);

                foreach (var p in parameters)
                {
                    AddParameters(command, p);

                    await _command.ExecuteNonQueryAsync(command);

                    command.Parameters.Clear();
                }
            }

            return true;
        }

        private IDbCommand AddParameters(IDbCommand command, IEnumerable<SqlParameter> parameters)
        {
            if (parameters != default(IEnumerable<SqlParameter>))
                foreach (var p in parameters)
                    command.Parameters.Add(p);

            return command;
        }

        private async Task<T> ExecuteReaderResult<T>(Func<IDataReader, T> loader, IDbCommand command, T result)
        {
            using (var reader = await _command.ExecuteReaderAsync(command))
                if (reader.Read())
                    result = loader(reader);
                else
                    reader.NextResult();

            return result;
        }

        private async Task<List<T>> ExecuteReaderResultForList<T>(Func<IDataReader, T> loader, IDbCommand command, List<T> result)
        {
            using (var reader = await _command.ExecuteReaderAsync(command))
                while (reader.Read())
                    result.Add(loader(reader));

            return result;
        }
    }
}
