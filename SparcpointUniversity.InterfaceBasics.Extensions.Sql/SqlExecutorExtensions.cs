using SparcpointUniversity.InterfaceBasics.Extensions.Sql;
using SparcpointUniversity.InterfaceBasics.Sql.Abstractions;
using System.Data;

namespace SparcpointUniversity.InterfaceBasics.Sql
{
    public static class SqlExecutorExtensions
    {
        public static ISqlExecutor WithTransaction(this ISqlExecutor executor)
            => new WithTransactionSqlExecutor(executor);

        public static ISqlExecutor WithTransaction(this ISqlExecutor executor, IsolationLevel isolationLevel)
            => new WithTransactionSqlExecutor(executor, isolationLevel);
    }
}
