using SparcpointUniversity.Sql.Abstractions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SparcpointUniversity.Sql.SqlServer
{
    public class SqlServerSqlExecutor : ISqlExecutor
    {
        private readonly string _ConnectionString;

        public SqlServerSqlExecutor(string connectionString)
        {
            _ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> executor)
        {
            using (var sqlConn = new SqlConnection(_ConnectionString))
            {
                await sqlConn.OpenAsync();
                return await executor(sqlConn, null);
            }
        }
    }
}
