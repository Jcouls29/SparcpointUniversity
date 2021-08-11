using SparcpointUniversity.InterfaceBasics.Sql.Abstractions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SparcpointUniversity.InterfaceBasics.Sql.SqlServer
{
    public class SqlServerSqlExecutor : ISqlExecutor
    {
        private readonly string _ConnectionString;

        public SqlServerSqlExecutor(string connectionString)
        {
            _ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public T Execute<T>(Func<IDbConnection, IDbTransaction, T> executor)
        {
            using(var sqlConn = new SqlConnection(_ConnectionString))
            {
                sqlConn.Open();
                return executor(sqlConn, null);
            }
        }

        public void Execute(Action<IDbConnection, IDbTransaction> executor)
        {
            using (var sqlConn = new SqlConnection(_ConnectionString))
            {
                sqlConn.Open();
                executor(sqlConn, null);
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> executor)
        {
            using (var sqlConn = new SqlConnection(_ConnectionString))
            {
                await sqlConn.OpenAsync();
                return await executor(sqlConn, null);
            }
        }

        public async Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> executor)
        {
            using (var sqlConn = new SqlConnection(_ConnectionString))
            {
                await sqlConn.OpenAsync();
                await executor(sqlConn, null);
            }
        }
    }
}
