using SparcpointUniversity.Extensions.Sql;
using SparcpointUniversity.Sql.Abstractions;

namespace SparcpointUniversity.Sql.Abstractions
{
    public static class SqlExecutorExtensions
    {
        public static ISqlExecutor WithTransaction(this ISqlExecutor executor)
            => new WithTransactionSqlExecutor(executor);
    }
}
